<template>
  <el-card class="signup-card">
    <h2 class="signup-title">Sign Up</h2>
    <el-form
      :model="signupForm"
      :rules="signupRules"
      ref="signupForm"
      label-width="0"
    >
      <el-form-item prop="username">
        <el-input
          v-model="signupForm.username"
          placeholder="User"
          class="input-field"
          prefix-icon="user"
        ></el-input>
      </el-form-item>
      <el-form-item prop="email">
        <el-input
          v-model="signupForm.email"
          placeholder="Email"
          class="input-field"
          prefix-icon="message"
        ></el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input
          v-model="signupForm.password"
          placeholder="Password"
          show-password
          class="input-field"
          prefix-icon="lock"
        ></el-input>
      </el-form-item>
      <el-form-item prop="confirmPassword">
        <el-input
          v-model="signupForm.confirmPassword"
          placeholder="Confirm Password"
          show-password
          class="input-field"
          prefix-icon="lock"
        ></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="register" class="signup-button"
          >Sign Up</el-button
        >
      </el-form-item>
    </el-form>
  </el-card>
</template>

<script lang="ts">
import axios from "axios";
import { ref } from "vue";
import { ElForm, ElFormItem, ElInput, ElButton, ElCard } from "element-plus";
import {
  signupEndPoint,
  loginEndPoint,
  sendEmailEndPoint,
} from "~/constants/endpoints";
import CryptoJS from "crypto-js";
import { useSessionStore } from "~/stores/session";
import { signUpEmailTemplate } from "~/constants/emails";

export default {
  components: {
    ElForm,
    ElFormItem,
    ElInput,
    ElButton,
    ElCard,
  },
  setup() {
    const session = useSessionStore();
    const signupForm = ref({
      username: "",
      email: "",
      password: "",
      confirmPassword: "",
    });

    const signupRules = ref({
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
        { validator: validateConfirmPassword, trigger: "blur" },
      ],
    });

    const validateConfirmPassword = (rule, value, callback) => {
      if (value !== signupForm.value.password) {
        callback(new Error("Passwords don´t match"));
      } else {
        callback();
      }
    };

    const encrypt = (text: string) => {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    };

    const register = async () => {
      try {
        const isValid = await ref("signupForm").value.validate();
        if (!isValid) return;

        const response = await axios.post(
          `${signupEndPoint}?username=${signupForm.value.username.toLowerCase()}&email=${
            signupForm.value.email
          }&password=${encrypt(signupForm.value.password)}`
        );

        await axios.post(
          `${sendEmailEndPoint}?mailFrom=${signupForm.value.email}&subject=${signUpEmailTemplate.subject}&body=${signUpEmailTemplate.body}`
        );

        await axios.post(
          `${loginEndPoint}?username=${signupForm.value.username.toLowerCase()}&password=${encrypt(
            signupForm.value.password
          )}`
        );
        const sessionStore = useSessionStore();
        sessionStore.login(signupForm.value.username);

        this.$router.push("/dashboard");

        console.log("Sign up correct:", response.data);
      } catch (error) {
        console.error("Error while creating the new user:", error);
      }
    };

    const validateFields = async () => {
      return await new Promise((resolve) => {
        const hasSpaces = /\s/.test(signupForm.value.password);
        const validLength = signupForm.value.password.length >= 5;
        const containsSpecialChar = /[!@#$%^&*]/.test(
          signupForm.value.password
        );
        const containsUpperCase = /[A-Z]/.test(signupForm.value.password);
        const containsLowerCase = /[a-z]/.test(signupForm.value.password);
        const containsNumber = /\d/.test(signupForm.value.password);

        const valid =
          signupForm.value.confirmPassword === signupForm.value.password &&
          !hasSpaces &&
          validLength &&
          containsSpecialChar &&
          containsUpperCase &&
          containsLowerCase &&
          containsNumber;

        resolve(valid);
      });
    };

    return {
      session,
      signupForm,
      signupRules,
      register,
      validateConfirmPassword,
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.signup-card {
  width: 300px;
  padding: $spacing-medium;

  .signup-title {
    text-align: center;
    font-size: $font-size-large;
    margin-bottom: $spacing-medium;
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

    &:hover {
      background-color: darken($color-primary, 10%);
    }
  }
}
</style>
