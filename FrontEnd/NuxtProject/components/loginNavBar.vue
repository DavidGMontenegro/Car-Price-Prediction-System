<template>
  <!-- Navbar container -->
  <div class="navbar" @click="toggleOption(false)">
    <!-- Left arrow -->
    <div class="arrow">
      <span v-if="selectedOption === 'SignUp'">&lt;</span>
    </div>
    <!-- Option text -->
    <div class="option">
      <span v-if="selectedOption === 'LogIn'">Sign Up</span>
      <span v-else>Login</span>
    </div>
    <!-- Right arrow -->
    <div class="arrow right">
      <span v-if="selectedOption === 'LogIn'">&gt;</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      // Initially selected option
      selectedOption: "LogIn",
    };
  },
  methods: {
    // Method to toggle between Login and Sign Up
    toggleOption() {
      this.selectedOption =
        this.selectedOption === "LogIn" ? "SignUp" : "LogIn";
      // Emitting event to parent component with the selected option
      this.$emit("option-selected", this.selectedOption);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: $color-nav-background;
  padding: $spacing-small $spacing-medium;
  cursor: pointer;
}

.option,
.arrow {
  color: #ffffff;
  min-width: 100px;
}

.right {
  text-align: right;
}

.arrow {
  $font-size-large: 24px;
}
</style>
