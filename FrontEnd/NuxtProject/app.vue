<template>
  <div class="app-container">
    <Nav></Nav>
    <NuxtPage class="nuxt-page" />
    <Footer />
  </div>
</template>

<script setup>
import Nav from "@/components/nav.vue";
import Footer from "@/components/footer.vue";
</script>

<style lang="scss">
@import "@/assets/styles/variables.scss";

body {
  font-family: "Cascadia code", sans-serif;
  margin: 0;
  padding: 0;
}

.app-container {
  background-color: $color-background;
  min-height: 5vh;
}

.nuxt-page {
  margin: 10px;
}
</style>
