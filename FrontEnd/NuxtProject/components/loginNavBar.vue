<template>
  <div class="navbar" @click="toggleOption(false)">
    <div class="arrow">
      <span v-if="selectedOption === 'SignUp'">&lt;</span>
    </div>
    <div class="option">
      <span v-if="selectedOption === 'LogIn'">Sign Up</span>
      <span v-else>Login</span>
    </div>
    <div class="arrow">
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

.arrow {
  font-size: 24px;
}
</style>
