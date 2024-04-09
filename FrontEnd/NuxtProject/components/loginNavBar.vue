<template>
  <div class="navbar">
    <div class="arrow" @click="toggleOption(false)">
      <span v-if="selectedOption === 'SignUp'">&lt;</span>
    </div>
    <div class="option" @click="toggleOption()">
      <span v-if="selectedOption === 'LogIn'">Sign Up</span>
      <span v-else>Login</span>
    </div>
    <div class="arrow" @click="toggleOption(true)">
      <span v-if="selectedOption === 'LogIn'">&gt;</span>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      selectedOption: "LogIn",
    };
  },
  methods: {
    toggleOption() {
      this.selectedOption =
        this.selectedOption === "LogIn" ? "SignUp" : "LogIn";
      this.$emit("option-selected", this.selectedOption);
    },
  },
};
</script>

<style scoped>
.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: $color-nav-background;
  padding: $spacing-small $spacing-medium;
}

.option,
.arrow {
  color: #ffffff;
  cursor: pointer;
  min-width: 100px;
}

.arrow {
  font-size: 24px;
}
</style>
