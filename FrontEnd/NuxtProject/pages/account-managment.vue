<template>
  <!-- Container for the entire component -->
  <div class="custom-container">
    <!-- Left section containing background image -->
    <div class="left-section">
      <img
        src="https://final-getd.s3.eu-west-2.amazonaws.com/black_porsche_port.avif"
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
import Navbar from "@/components/login/loginNavBar.vue";
import loginForm from "@/components/login/loginForm.vue";
import signupForm from "@/components/login/signupForm.vue";

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
    background-image: url("https://final-getd.s3.eu-west-2.amazonaws.com/black_porsche_port.avif");
    background-size: contain;
    background-position: center;
    background-repeat: no-repeat;
  }
}
</style>
