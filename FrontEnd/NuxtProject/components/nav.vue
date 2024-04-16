<template>
  <el-menu
    :default-active="activeIndex"
    class="navbar"
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

    <el-menu-item index="4" v-if="session.isLoggedIn" route="/personal-data"
      >My data</el-menu-item
    >

    <el-menu-item
      index="5"
      route="/"
      v-if="session.isLoggedIn"
      @click="session.logout()"
      ><img
        src="https://static-00.iconduck.com/assets.00/logout-icon-2048x2046-yqonjwjv.png"
        alt="Cerrar sesión"
        class="logout-icon"
    /></el-menu-item>
  </el-menu>
</template>

<script lang="ts">
import { useSessionStore } from "~/stores/session";

export default {
  setup() {
    return { session: useSessionStore() };
  },
  data() {
    return {
      activeIndex: "1",
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.navbar {
  background-color: $color-nav-background;
  border: none;
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

.navbar .el-menu-item:hover {
  background-color: darken($color-nav-background, 5%);
}

.logout-icon {
  width: 24px;
  height: 24px;
}
</style>
