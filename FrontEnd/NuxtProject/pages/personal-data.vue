<template>
  <div class="custom-container">
    <div class="left-section" v-if="isWideEnough">
      <img
        src="https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="Background"
        class="background-image"
      />
    </div>
    <div class="right-section centered">
      <div class="content">
        <div v-if="!changePassword" class="user-data-container">
          <PersonalDataDisplay />
          <button class="change-password-button" @click="changePassword = true">
            Cambiar contraseña
          </button>
        </div>
        <div v-else>
          <ChangePassword @cancel="changePassword = false" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import PersonalDataDisplay from "@/components/PersonalDataDisplay.vue";
import ChangePassword from "@/components/ChangePassword.vue";

export default {
  data() {
    definePageMeta({
      middleware: "auth",
    });

    return {
      changePassword: false,
      isWideEnough: false,
    };
  },
  methods: {
    handleResize() {
      this.isWideEnough = window.innerWidth > 768;
    },
  },
  mounted() {
    this.isWideEnough = window.innerWidth > 768;
    window.addEventListener("resize", this.handleResize);
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.handleResize);
  },
};
</script>

<style scoped>
.custom-container {
  display: flex;
  height: 100vh;
}

.left-section {
  flex: 1;
}

.background-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.right-section {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}

.content {
  text-align: center;
  padding: 20px;
}

.user-data-container {
  max-width: 400px;
  margin: 0 auto;
}

.change-password-button {
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  margin-top: 20px;
}

.change-password-button:hover {
  background-color: #45a049;
}
</style>
