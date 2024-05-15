<template>
  <!-- Password change form component -->
  <div class="password-change-form">
    <div class="password-change-buttons">
      <!-- Button to cancel password change -->
      <el-button @click="$emit('cancel')" class="password-change-button">
        <!-- Icon for cancel button -->
        <img
          src="https://final-getd.s3.eu-west-2.amazonaws.com/UI_Icons/GoBackArrow.png"
          alt="Cancelar"
          class="button-icon"
        />
      </el-button>
      <!-- Title for password change form -->
      <p>Change Password</p>
      <!-- Button to save password changes -->
      <el-button @click="saveChanges" class="password-change-button">
        <!-- Icon for save button -->
        <img
          src="https://final-getd.s3.eu-west-2.amazonaws.com/UI_Icons/Done.png"
          alt="Guardar"
          class="button-icon"
        />
      </el-button>
    </div>
    <!-- Form for changing password -->
    <el-form
      label-position="top"
      style="max-width: 600px"
      :rules="changePasswordRules"
    >
      <!-- Input for current password -->
      <el-form-item label="Current Password" prop="actualPassword">
        <el-input
          type="password"
          v-model="currentPassword"
          clearable
          placeholder="Current Password"
        ></el-input>
      </el-form-item>
      <!-- Input for new password -->
      <el-form-item label="New Password" prop="nwePassword">
        <el-input
          type="password"
          v-model="newPassword"
          clearable
          placeholder="New Password"
        ></el-input>
      </el-form-item>
      <!-- Input for confirming new password -->
      <el-form-item label="Confirm New Password" prop="repeatPassword">
        <el-input
          type="password"
          v-model="confirmPassword"
          clearable
          placeholder="Confirm New Password"
        ></el-input>
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts">
// Importing necessary modules and libraries
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
    // Define reactive variables for password inputs and session
    const currentPassword = ref("");
    const newPassword = ref("");
    const confirmPassword = ref("");
    const session = useSessionStore();

    // Function to encrypt password
    const encrypt = (text: string) => {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    };

    // Function to save password changes
    const saveChanges = async () => {
      // Validate form fields
      const valid = await validateFields();

      if (valid) {
        try {
          // Send request to update password
          const response = await axios.put(
            `${changePasswordEndPoint}?username=${
              session.username
            }&oldPassword=${encrypt(
              currentPassword.value
            )}&newPassword=${encrypt(newPassword.value)}`
          );

          // Send confirmation email
          await axios.post(
            `${sendEmailEndPoint}?mailFrom=${session.username}&subject=${signUpEmailTemplate.subject}&body=${signUpEmailTemplate.body}`
          );

          // Clear fields after successful password change
          currentPassword.value = "";
          newPassword.value = "";
          confirmPassword.value = "";
        } catch (error) {
          // Display error message if request fails
          this.$message.error("An error ocurred. Try again later...");
        }
      }
    };

    // Function to validate form fields
    const validateFields = async () => {
      return await new Promise((resolve) => {
        // Validation logic
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
      // Rules for form validation
      changePasswordRules: {
        oldPassword: [
          {
            required: true,
            message: "Please enter your username",
            trigger: "blur",
          },
          {
            min: 6,
            message: "Password must be at least 6 characters long",
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
            message: "Password must be at least 6 characters long",
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
            message: "Password must be at least 6 characters long",
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
  padding: $spacing-small;
  margin-bottom: 30px;
  align-items: center;
}

.password-change-button {
  background-color: $color-primary;
  color: white;
  border: none;
  border-radius: 4px;
  padding: $spacing-small $spacing-large;
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
