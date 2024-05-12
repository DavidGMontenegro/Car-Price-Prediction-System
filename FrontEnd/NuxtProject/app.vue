<template>
  <div class="app-container">
    <Nav />
    <NuxtPage class="nuxt-page" />
    <Footer />
  </div>
</template>

<script setup>
import Nav from "@/components/nav.vue";
import Footer from "@/components/footer.vue";
import { useSessionStore } from "~/stores/session";

const sessionStore = useSessionStore();

async function handleLogin() {
  if (process.client) {
    const username = localStorage.getItem("username");
    if (username) {
      try {
        sessionStore.login(username);
        //emit("load-profile-image");
      } catch (error) {
        console.error(`Login error: ${error.message}`);
      }
    }
  }
}

handleLogin();
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
