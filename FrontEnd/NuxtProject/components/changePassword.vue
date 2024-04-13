<template>
  <div class="password-change-form">
    <div class="password-change-buttons">
      <el-button @click="cancelChanges" class="password-change-button">
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
    <el-form label-position="top" style="max-width: 600px" ref="form">
      <el-form-item label="Contraseña actual">
        <el-input
          type="password"
          v-model="currentPassword"
          clearable
          placeholder="Contraseña actual"
        ></el-input>
      </el-form-item>
      <el-form-item label="Nueva contraseña">
        <el-input
          type="password"
          v-model="newPassword"
          clearable
          placeholder="Nueva contraseña"
        ></el-input>
      </el-form-item>
      <el-form-item label="Confirmar nueva contraseña">
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
import { ref, defineEmits } from "vue";
import axios from "axios";
import { changePasswordEndPoint } from "~/constants/endpoints";
import { useSessionStore } from "~/stores/session";
import CryptoJS from "crypto-js";

export default {
  setup(_, { emit }) {
    const emits = defineEmits(["cancel"]);
    const currentPassword = ref("");
    const newPassword = ref("");
    const confirmPassword = ref("");
    const session = useSessionStore();

    const encrypt = (text: string) => {
      return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(text));
    };

    const cancelChanges = () => {
      // Emitir evento de cancelación
      // Asegúrate de tener este evento definido donde uses este componente
      emit("cancel");
    };

    const saveChanges = async () => {
      // Validar los campos del formulario
      const valid = await validateFields();

      if (valid) {
        try {
          console.log(session.username);
          const response = await axios.put(
            `${changePasswordEndPoint}?username=${
              session.username
            }&oldPassword=${encrypt(
              currentPassword.value
            )}&newPassword=${encrypt(newPassword.value)}`
          );

          // Limpiar los campos después de un cambio de contraseña exitoso
          currentPassword.value = "";
          newPassword.value = "";
          confirmPassword.value = "";
        } catch (error) {
          console.error("Failed to fetch user data", error);
        }
        emit("cancel");
      }
    };

    const validateFields = async () => {
      return await new Promise((resolve) => {
        // Validar los campos del formulario
        // Aquí puedes implementar la lógica de validación que necesites
        // Por ejemplo, puedes verificar que los campos no estén vacíos y que las contraseñas coincidan
        const valid =
          currentPassword.value !== "" &&
          newPassword.value !== "" &&
          confirmPassword.value !== "" &&
          newPassword.value === confirmPassword.value;
        resolve(valid);
      });
    };

    return {
      currentPassword,
      newPassword,
      confirmPassword,
      cancelChanges,
      saveChanges,
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
</style>
