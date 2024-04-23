<template>
  <div class="password-change-form">
    <div class="password-change-buttons">
      <el-button @click="$emit('cancel')" class="password-change-button">
        <img
          src="@/assets/images/GoBackArrow.png"
          alt="Cancelar"
          class="button-icon"
        />
      </el-button>
      <p>Change Password</p>
      <el-button @click="saveChanges" class="password-change-button">
        <img src="@/assets/images/Done.png" alt="Guardar" class="button-icon" />
      </el-button>
    </div>
    <el-form
      label-position="top"
      style="max-width: 600px"
      :rules="changePasswordRules"
    >
      <el-form-item label="Contraseña actual" prop="actualPassword">
        <el-input
          type="password"
          v-model="currentPassword"
          clearable
          placeholder="Contraseña actual"
        ></el-input>
      </el-form-item>
      <el-form-item label="Nueva contraseña" prop="nwePassword">
        <el-input
          type="password"
          v-model="newPassword"
          clearable
          placeholder="Nueva contraseña"
        ></el-input>
      </el-form-item>
      <el-form-item label="Confirmar nueva contraseña" prop="repeatPassword">
        <el-input
          type="password"
          v-model="confirmPassword"
          clearable
          placeholder="Confirmar nueva contraseña"
        ></el-input>
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts">
import { ref } from "vue";
import axios from "axios";
import {
  changePasswordEndPoint,
  sendEmailEndPoint,
} from "~/constants/endpoints";
import { useSessionStore } from "~/stores/session";
import CryptoJS from "crypto-js";
import { signUpEmailTemplate } from "~/constants/emails";

export default {
  setup() {
    const currentPassword = ref("");
    const newPassword = ref("");
    const confirmPassword = ref("");
    const session = useSessionStore();

    const encrypt = (text: string) => {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    };

    const saveChanges = async () => {
      const valid = await validateFields();

      if (valid) {
        try {
          const response = await axios.put(
            `${changePasswordEndPoint}?username=${
              session.username
            }&oldPassword=${encrypt(
              currentPassword.value
            )}&newPassword=${encrypt(newPassword.value)}`
          );

          await axios.post(
            `${sendEmailEndPoint}?mailFrom=${session.username}&subject=${signUpEmailTemplate.subject}&body=${signUpEmailTemplate.body}`
          );

          // Limpiar los campos después de un cambio de contraseña exitoso
          currentPassword.value = "";
          newPassword.value = "";
          confirmPassword.value = "";
        } catch (error) {
          console.error("Failed to fetch user data", error);
        }
      }
    };

    const validateFields = async () => {
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
    };

    return {
      currentPassword,
      newPassword,
      confirmPassword,
      saveChanges,
      changePasswordRules: {
        oldPassword: [
          {
            required: true,
            message: "Please enter your username",
            trigger: "blur",
          },
          {
            min: 6,
            message: "La contraseña debe tener al menos 6 caracteres",
            trigger: "blur",
          },
        ],
        newPassword: [
          {
            required: true,
            message: "Please enter your password",
            trigger: "blur",
          },
          {
            min: 6,
            message: "La contraseña debe tener al menos 6 caracteres",
            trigger: "blur",
          },
        ],
        repeatPassword: [
          {
            required: true,
            message: "Please enter your password",
            trigger: "blur",
          },
          {
            min: 6,
            message: "La contraseña debe tener al menos 6 caracteres",
            trigger: "blur",
          },
        ],
      },
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.password-change-form {
  display: flex;
  flex-direction: column;
  background-color: $color-secondary;
  padding: $spacing-medium;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  min-width: 35vw;
  min-height: 50vh;
  justify-content: center;
}

.el-button {
  border-radius: 4px;
  padding: $spacing-small $spacing-medium;
}

.button-icon {
  width: 24px;
  height: 24px;
}

.password-change-buttons {
  display: flex;
  justify-content: space-between;
  padding: 10px;
  margin-bottom: 30px;
  align-items: center;
}

.password-change-button {
  background-color: $color-primary;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 8px 16px;
  margin-top: 0px;
  cursor: pointer;

  &:hover {
    background-color: darken($color-primary, 10%);
  }
}

@media (max-width: 768px) {
  .password-change-form {
    min-width: 70vw;
  }
}
</style>
