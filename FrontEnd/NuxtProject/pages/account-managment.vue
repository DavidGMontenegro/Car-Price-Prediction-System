<template>
  <div class="custom-container">
    <div class="left-section" v-if="isWideEnough">
      <img
        src="https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="Background"
        class="background-image"
      />
    </div>
    <div class="right-section" :class="{ centered: !isWideEnough }">
      <div class="content">
        <loginForm v-if="selectedOption === 'LogIn'" />
        <signupForm v-else-if="selectedOption === 'SignUp'" />
        <Navbar @option-selected="handleOptionSelected" />
      </div>
    </div>
  </div>
</template>

<script>
import Navbar from "@/components/loginNavBar.vue";
import loginForm from "@/components/loginForm.vue";
import signupForm from "@/components/signupForm.vue";

export default {
  components: { Navbar, loginForm, signupForm },
  data() {
    return {
      selectedOption: "LogIn",
      isWideEnough: window.innerWidth > 768,
    };
  },
  methods: {
    handleResize() {
      this.isWideEnough = window.innerWidth > 768;
    },
    handleOptionSelected(option) {
      this.selectedOption = option;
    },
  },
  mounted() {
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

.right-section.centered {
  justify-content: center;
}

.content {
  text-align: center;
  padding: 20px;
}
</style>
