<template>
  <el-menu
    :default-active="activeIndex"
    class="custom-nav"
    mode="horizontal"
    background-color="#222831"
    text-color="#fff"
    router
  >
    <el-menu-item index="1" route="/">Inicio</el-menu-item>
    <el-menu-item index="2" v-if="session.isLoggedIn" route="/dashboard"
      >Dashboard</el-menu-item
    >
    <el-menu-item
      index="3"
      v-if="!session.isLoggedIn"
      route="/account-managment"
      >Log In</el-menu-item
    >
    <el-menu-item index="4" route="/" v-if="session.isLoggedIn" @click="logout"
      >Cerrar sesión</el-menu-item
    >
  </el-menu>
</template>

<script lang="ts">
import { useSessionStore } from "~/stores/session";

export default {
  setup() {
    const session = useSessionStore();
    return { session };
  },
  data() {
    return {
      activeIndex: "1",
    };
  },
  methods: {
    logout() {
      this.session.logout();
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.custom-nav {
  background-color: $color-nav-background;
  border: none;
  height: 10vh;
}

.links {
  text-decoration: none;
}

.el-menu-item {
  font-size: $font-size-medium;
  padding: $spacing-small $spacing-medium;
  color: $color-secondary;
  height: 100%;
}

.custom-nav .el-menu-item:hover {
  background-color: darken($color-nav-background, 5%);
}
</style>
