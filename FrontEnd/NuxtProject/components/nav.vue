<template>
  <el-menu
    :default-active="activeIndex"
    class="navbar"
    mode="horizontal"
    background-color="#222831"
    text-color="#fff"
    router
  >
    <div class="button-container">
      <el-menu-item index="1" route="/">Home</el-menu-item>
      <el-menu-item index="2" v-if="session.isLoggedIn" route="/dashboard"
        >Dashboard</el-menu-item
      >
      <el-menu-item
        index="3"
        v-if="!session.isLoggedIn"
        route="/account-managment"
        >Log In</el-menu-item
      >
      <el-menu-item
        index="3"
        v-if="session.isLoggedIn"
        route="/price-prediction"
        >Price Prediction</el-menu-item
      >
    </div>
    <div class="button-container">
      <el-menu-item index="4" v-if="session.isLoggedIn" route="/personal-data">
        <img :src="profileImageUrl" alt="Profile" class="profile-image" />
      </el-menu-item>
      <el-menu-item
        index="5"
        route="/"
        v-if="session.isLoggedIn"
        @click="session.logout()"
      >
        <img src="assets/images/LogOut.png" alt="Log Out" class="logout-icon" />
      </el-menu-item>
    </div>
  </el-menu>
</template>

<script lang="ts">
import { useSessionStore } from "~/stores/session";
import { ref, onMounted, watch } from "vue";
import axios from "axios";
import { getUserDataEndPoint } from "~/constants/endpoints";

export default {
  setup() {
    const session = useSessionStore();
    const profileImageUrl = ref("");

    const fetchUserData = async () => {
      try {
        const response = await axios.get(
          `${getUserDataEndPoint}?username=${session.username}`
        );
        profileImageUrl.value = `data:image/png;base64,${response.data.profilePicture}`;
      } catch (error) {
        console.error("Failed to fetch user data: ", error);
      }
    };

    // Fetch data only on initial mount and when username changes
    watch(
      () => session.username,
      async (username) => {
        if (username && session.isLoggedIn) {
          await fetchUserData();
        } else {
          profileImageUrl.value = ""; // Clear image if not logged in or username changes
        }
      },
      { immediate: true } // Fetch data on initial mount
    );

    return { session, profileImageUrl };
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
  display: flex;
  justify-content: space-between;
}

.links {
  text-decoration: none;
}

.button-container {
  display: flex;
  flex-direction: row;
  padding-left: 20px;
  padding-right: 20px;
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
  width: 20px;
  height: 20px;
  margin-left: 10px;
  margin-right: 10px;
}

.profile-image {
  width: 45px;
  height: 45px;
  border-radius: 50%;
}
</style>
