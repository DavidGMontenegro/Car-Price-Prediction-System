<template>
  <!-- Container for the entire component -->
  <div class="custom-container">
    <!-- Left section containing background image -->
    <div class="left-section">
      <img
        src="https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="Background"
        class="background-image"
      />
    </div>
    <!-- Right section containing login/signup forms and navbar -->
    <div class="right-section centered">
      <div class="content">
        <!-- Display login form if selected option is 'LogIn' -->
        <loginForm v-if="selectedOption === 'LogIn'" />
        <!-- Display signup form if selected option is 'SignUp' -->
        <signupForm v-else-if="selectedOption === 'SignUp'" />
      </div>
      <!-- Navbar component for selecting login/signup -->
      <Navbar @option-selected="handleOptionSelected" />
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
      // Default selected option is 'LogIn'
      selectedOption: "LogIn",
    };
  },
  methods: {
    // Handle option selection from the navbar
    handleOptionSelected(option) {
      this.selectedOption = option;
    },
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
  flex-direction: column;
}

.content {
  text-align: center;
  min-height: 45vh;
}

/* Media query for responsiveness */
@media (max-width: 768px) {
  .left-section {
    display: none; /* Hide left section on smaller screens */
  }

  .right-section {
    flex: none;
    width: 100%;
    background-image: url("https://images.unsplash.com/photo-1580274455191-1c62238fa333?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
    background-size: contain;
    background-position: center;
    background-repeat: no-repeat;
  }
}
</style>
