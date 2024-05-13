<template>
  <!-- Login container -->
  <div class="login-container">
    <div class="right-section">
      <!-- Login card -->
      <el-card class="login-card">
        <!-- Login title -->
        <h2 class="login-title">Log In</h2>
        <!-- Login form -->
        <el-form
          ref="loginForm"
          :model="loginForm"
          :rules="loginRules"
          label-width="0"
          class="login-form"
        >
          <!-- Username input -->
          <el-form-item prop="username" class="form-item">
            <el-input
              v-model="loginForm.username"
              placeholder="Username"
              class="input-field"
              prefix-icon="User"
            ></el-input>
          </el-form-item>
          <!-- Password input -->
          <el-form-item prop="password" class="form-item">
            <el-input
              v-model="loginForm.password"
              placeholder="Password"
              show-password
              class="input-field"
              prefix-icon="lock"
            ></el-input>
          </el-form-item>
          <!-- Login button -->
          <el-form-item>
            <el-button type="primary" @click="login" class="login-button">
              Log In
            </el-button>
          </el-form-item>
        </el-form>
      </el-card>
    </div>
  </div>
</template>

<script lang="ts">
// Importing necessary modules and constants
import { loginEndPoint } from "~/constants/endpoints";
import axios from "axios";
import CryptoJS from "crypto-js";
import { useSessionStore } from "~/stores/session";

export default {
  setup() {
    // Getting session store
    const session = useSessionStore();
    return { session };
  },
  data() {
    return {
      // Form data and validation rules
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
    // Method to encrypt password
    encrypt(text: string) {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    },
    // Method to handle login
    async login() {
      try {
        // Sending login request
        const response = await axios.post(
          `${loginEndPoint}?username=${this.loginForm.username.toLowerCase()}&password=${this.encrypt(
            this.loginForm.password
          )}`
        );
        // Updating session and redirecting on successful login
        const sessionStore = useSessionStore();
        sessionStore.login(this.loginForm.username);
        this.$router.push("/dashboard");

        // Handle backend response, e.g., store session token in local storage or redirect to another page.
      } catch (error) {
        // Handling login failure
        console.error("Failed login", error);
        if (error.response.status === 401) {
          this.$message.error("Invalid username or password");
          this.loginForm.password = ""; // Clear password
        }
      }
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
</style>
