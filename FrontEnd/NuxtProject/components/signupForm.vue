<template>
  <div class="signup">
    <el-card class="signup-card">
      <h2 class="signup-title">Sign Up</h2>
      <el-form
        ref="signupForm"
        :model="signupForm"
        :rules="signupRules"
        label-width="0"
      >
        <el-form-item prop="username" class="form-item">
          <el-input
            v-model="signupForm.username"
            placeholder="User"
            class="input-field"
            prefix-icon="user"
          ></el-input>
        </el-form-item>
        <el-form-item prop="email" class="form-item">
          <el-input
            v-model="signupForm.email"
            placeholder="Email"
            class="input-field"
            prefix-icon="message"
          ></el-input>
        </el-form-item>
        <el-form-item prop="password" class="form-item">
          <el-input
            v-model="signupForm.password"
            placeholder="Password"
            show-password
            class="input-field"
            prefix-icon="lock"
          ></el-input>
        </el-form-item>
        <el-form-item prop="confirmPassword" class="form-item">
          <el-input
            v-model="signupForm.confirmPassword"
            placeholder="Confirm Password"
            show-password
            class="input-field"
            prefix-icon="lock"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="register" class="signup-button">
            Sign Up
          </el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <div v-if="error" class="error">{{ error }}</div>
    <!-- Mostrar errores -->
  </div>
</template>

<script>
import axios from "axios";
import {
  signupEndPoint,
  sendEmailEndPoint,
  loginEndPoint,
} from "~/constants/endpoints";
import CryptoJS from "crypto-js";
import { signUpEmailTemplate } from "~/constants/emails";
import { useSessionStore } from "~/stores/session";

export default {
  setup() {
    const session = useSessionStore();
    return { session };
  },
  data() {
    return {
      signupForm: {
        username: "",
        email: "",
        password: "",
        confirmPassword: "",
      },
      signupRules: {
        username: [
          {
            required: true,
            message: "Please, introduce a username",
            trigger: "blur",
          },
        ],
        email: [
          {
            required: true,
            message: "Please, introduce an email",
            trigger: "blur",
          },
          {
            type: "email",
            message: "Please, introduce a valid email",
            trigger: "blur,change",
          },
        ],
        password: [
          {
            required: true,
            message: "Please, introduce a password",
            trigger: "blur",
          },
          {
            min: 6,
            message: "Must contain at least 6 characters",
            trigger: "blur",
          },
          {
            validator: this.validatePassword,
            trigger: "blur",
          },
        ],
        confirmPassword: [
          {
            required: true,
            message: "Please, repeat your password",
            trigger: "blur",
          },
          {
            validator: this.validateConfirmPassword,
            trigger: "blur",
          },
        ],
      },
      error: "", // Variable para almacenar los errores
    };
  },
  methods: {
    encrypt(text) {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    },
    async register() {
      this.$refs.signupForm.validate(async (valid) => {
        if (valid) {
          const response = await axios.post(
            `${signupEndPoint}?username=${this.signupForm.username.toLowerCase()}&email=${
              this.signupForm.email
            }&password=${this.encrypt(this.signupForm.password)}`
          );

          await axios.post(
            `${sendEmailEndPoint}?mailFrom=${this.signupForm.email}&subject=${signUpEmailTemplate.subject}&body=${signUpEmailTemplate.body}`
          );

          await axios.post(
            `${loginEndPoint}?username=${this.signupForm.username.toLowerCase()}&password=${this.encrypt(
              this.signupForm.password
            )}`
          );

          const sessionStore = useSessionStore();
          sessionStore.login(this.signupForm.username.toLowerCase());

          this.$router.push("/dashboard");
        } else {
          console.log("Not valid form");
        }
      });
    },
    validateConfirmPassword(rule, value, callback) {
      if (value !== this.signupForm.password) {
        callback(new Error("Passwords don´t match"));
      } else {
        callback();
      }
    },
    validatePassword(rule, value, callback) {
      const hasUpperCase = /[A-Z]/.test(value);
      const hasLowerCase = /[a-z]/.test(value);
      const hasSpecialChar = /[\W_]/.test(value);

      if (!hasUpperCase) {
        callback(new Error("Must contain at least one uppercase"));
      } else if (!hasLowerCase) {
        callback(new Error("Must contain at least one lowercase"));
      } else if (!hasSpecialChar) {
        callback(new Error("Must contain special characters"));
      } else {
        callback();
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.signup {
  display: flex;
  justify-content: center;
  align-items: center;
}

.signup-card {
  width: 300px;
  padding: $spacing-medium;
}

.signup-title {
  text-align: center;
  font-size: $font-size-large;
  margin-bottom: $spacing-medium;
}

.form-item {
  margin-bottom: $spacing-small;
}

.input-field {
  border-radius: 0;
  font-size: $font-size-small;
  margin-top: $spacing-medium;
}

.signup-button {
  width: 100%;
  border-radius: 0;
  background-color: $color-primary;
  transition: background-color 0.3s ease;
  margin-top: $spacing-medium;
  color: black;
}

.signup-button:hover {
  background-color: darken($color-primary, 10%);
}

.error {
  color: red;
  text-align: center;
  margin-top: 10px;
}
</style>
