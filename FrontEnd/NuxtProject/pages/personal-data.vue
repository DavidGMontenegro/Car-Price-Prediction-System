<template>
  <div class="custom-container">
    <div class="left-section">
      <img
        src="https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="Background"
        class="background-image"
      />
    </div>
    <div class="right-section">
      <div v-if="!changePassword" class="user-data-container">
        <PersonalDataDisplay />
        <button class="change-password-button" @click="changePassword = true">
          Change password
        </button>
      </div>
      <div v-else>
        <ChangePassword @cancel="changePassword = false" />
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
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

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
  text-align: center;
  padding: $spacing-large;
  width: 500px;
}

.user-data-container {
  margin: 0 auto;
  min-width: 35vw;
}

.change-password-button {
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  padding: $spacing-small $spacing-large;
  font-size: $font-size-medium;
  cursor: pointer;
  transition: background-color 0.3s ease;
  margin-top: $spacing-large;
}

.change-password-button:hover {
  background-color: #45a049;
}

@media (max-width: 768px) {
  .left-section {
    flex: 0;
  }

  .right-section {
    background-image: url("https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
    background-size: contain;
    background-position: center;
    background-repeat: no-repeat;
  }

  .user-data-container {
    width: 60vw;
  }
}
</style>
