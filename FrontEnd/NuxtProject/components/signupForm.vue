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
  </div>
</template>

<script lang="ts">
import axios from "axios";
import { signupEndPoint } from "~/constants/endpoints";
import CryptoJS from "crypto-js";

export default {
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
        ],
        confirmPassword: [
          {
            required: true,
            message: "Please, repeat your password",
            trigger: "blur",
          },
          { validator: this.validateConfirmPassword, trigger: "blur" },
        ],
      },
      isWideEnough: false, // Inicializar como falso
    };
  },
  methods: {
    encrypt(text) {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    },
    async register() {
      this.$refs.signupForm.validate(async (valid) => {
        if (valid) {
          axios
            .post(signupEndPoint, {
              username: this.signupForm.username,
              email: this.signupForm.email,
              password: this.encrypt(this.signupForm.password),
            })
            .then((response) => {
              console.log("Sing up correct:", response.data);
            })
            .catch((error) => {
              console.error(
                "Error while creating the new user:",
                error.response.data
              );
            });
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
    handleResize() {
      this.isWideEnough = window.innerWidth > 768;
    },
  },
  mounted() {
    this.isWideEnough = window.innerWidth > 768; // Inicializar isWideEnough después de que el componente se monte
    window.addEventListener("resize", this.handleResize);
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.handleResize);
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.signup {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 65vh;
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
</style>
