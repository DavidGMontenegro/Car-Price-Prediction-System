<template>
  <div class="login-container">
    <div class="right-section">
      <el-card class="login-card">
        <h2 class="login-title">Log In</h2>
        <el-form
          ref="loginForm"
          :model="loginForm"
          :rules="loginRules"
          label-width="0"
          class="login-form"
        >
          <el-form-item prop="username" class="form-item">
            <el-input
              v-model="loginForm.username"
              placeholder="Username"
              class="input-field"
              prefix-icon="User"
            ></el-input>
          </el-form-item>
          <el-form-item prop="password" class="form-item">
            <el-input
              v-model="loginForm.password"
              placeholder="Password"
              show-password
              class="input-field"
              prefix-icon="lock"
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="login" class="login-button">
              Log In
            </el-button>
          </el-form-item>
        </el-form>

        <div class="links-container">
          <router-link to="/forgot-password" class="link">
            Recover Password
          </router-link>
        </div>
      </el-card>
    </div>
  </div>
</template>

<script lang="ts">
import { loginEndPoint } from "~/constants/endpoints";
import axios from "axios";
import CryptoJS from "crypto-js";
import { useSessionStore } from "~/stores/session";

export default {
  setup() {
    const session = useSessionStore();
    return { session };
  },
  data() {
    return {
      loginForm: {
        username: "",
        password: "",
      },
      loginRules: {
        username: [
          {
            required: true,
            message: "Please enter your username",
            trigger: "blur",
          },
        ],
        password: [
          {
            required: true,
            message: "Please enter your password",
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    encrypt(text: String) {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    },
    async login() {
      try {
        const response = await axios.post(
          `${loginEndPoint}?username=${this.loginForm.username.toLowerCase()}&password=${this.encrypt(
            this.loginForm.password
          )}`
        );

        const sessionStore = useSessionStore();
        sessionStore.login(this.loginForm.username);
        console.log("Login successful:", response.data);
        this.$router.push("/dashboard");

        // Aquí puedes manejar la respuesta del backend, como almacenar el token de sesión en el almacenamiento local o redirigir a otra página.
      } catch (error) {
        console.error("Failed login", error);
        // Aquí puedes manejar el error, como mostrar un mensaje al usuario.
      }
    },
    async validateFields() {
      return await new Promise((resolve) => {
        const hasSpaces = /\s/.test(newPassword.value);
        const validLength = newPassword.value.length >= 5;
        const containsSpecialChar = /[!@#$%^&*]/.test(newPassword.value);
        const containsUpperCase = /[A-Z]/.test(newPassword.value);
        const containsLowerCase = /[a-z]/.test(newPassword.value);
        const containsNumber = /\d/.test(newPassword.value);

        const valid =
          newPassword.value === confirmPassword.value &&
          !hasSpaces &&
          validLength &&
          containsSpecialChar &&
          containsUpperCase &&
          containsLowerCase &&
          containsNumber;

        resolve(valid);
      });
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.login-container {
  display: flex;
}

.right-section {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}

.login-card {
  width: 300px;
  padding: $spacing-medium;
}

.login-title {
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

.login-button {
  width: 100%;
  border-radius: 0;
  background-color: $color-primary;
  transition: background-color 0.3s ease;
  margin-top: $spacing-medium;
  color: black;
}

.login-button:hover {
  background-color: darken($color-primary, 10%);
}

.links-container {
  margin-top: $spacing-small;
  display: flex;
  flex-direction: column;
  text-align: center;
}

.link {
  color: darken($color-primary, 25%);
  margin-right: $spacing-small;
  text-decoration: none;
}

.link:last-child {
  margin-right: 0;
}
</style>
