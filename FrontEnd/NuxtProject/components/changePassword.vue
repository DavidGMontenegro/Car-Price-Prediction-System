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
          v-model="form.currentPassword"
          clearable
          placeholder="Contraseña actual"
        ></el-input>
      </el-form-item>
      <el-form-item label="Nueva contraseña">
        <el-input
          type="password"
          v-model="form.newPassword"
          clearable
          placeholder="Nueva contraseña"
        ></el-input>
      </el-form-item>
      <el-form-item label="Confirmar nueva contraseña">
        <el-input
          type="password"
          v-model="form.confirmPassword"
          clearable
          placeholder="Confirmar nueva contraseña"
        ></el-input>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        currentPassword: "",
        newPassword: "",
        confirmPassword: "",
      },
      rules: {
        currentPassword: [
          {
            required: true,
            message: "Por favor ingrese la contraseña actual",
            trigger: "blur",
          },
        ],
        newPassword: [
          {
            required: true,
            message: "Por favor ingrese la nueva contraseña",
            trigger: "blur",
          },
        ],
        confirmPassword: [
          {
            required: true,
            message: "Por favor confirme la contraseña",
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value !== this.form.newPassword) {
                callback(new Error("Las dos contraseñas no coinciden"));
              } else {
                callback();
              }
            },
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    submitForm() {
      this.$refs["form"].validate((valid) => {
        if (valid) {
          // Aquí puedes enviar la solicitud de cambio de contraseña al backend
          // Por simplicidad, aquí solo se muestra un mensaje de éxito
          this.$message.success("Contraseña cambiada exitosamente!");
          // Limpiar los campos del formulario después de enviar
          this.form.currentPassword = "";
          this.form.newPassword = "";
          this.form.confirmPassword = "";
        }
      });
    },
    cancelChanges() {
      this.$emit("cancel");
    },
    saveChanges() {
      this.$emit("cancel");
    },
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
