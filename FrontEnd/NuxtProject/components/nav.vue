<template>
  <!-- Navigation menu -->
  <el-menu
    :default-active="activeIndex"
    class="navbar"
    mode="horizontal"
    background-color="#222831"
    text-color="#fff"
    router
  >
    <!-- Left button container -->
    <div class="button-container">
      <!-- Home button -->
      <el-menu-item index="1" route="/">Home</el-menu-item>
      <!-- Dashboard button (visible if logged in) -->
      <el-menu-item index="2" v-if="session.isLoggedIn" route="/dashboard"
        >Dashboard</el-menu-item
      >
      <!-- Log In button (visible if not logged in) -->
      <el-menu-item
        index="3"
        v-if="!session.isLoggedIn"
        route="/account-managment"
        >Log In</el-menu-item
      >
      <!-- Price Prediction button (visible if logged in) -->
      <el-menu-item
        index="3"
        v-if="session.isLoggedIn"
        route="/price-prediction"
        >Price Prediction</el-menu-item
      >
    </div>
    <!-- Right button container -->
    <div class="button-container">
      <!-- Profile image (visible if logged in) -->
      <el-menu-item index="4" v-if="session.isLoggedIn" route="/personal-data">
        <img :src="profileImageUrl" alt="Profile" class="profile-image" />
      </el-menu-item>
      <!-- Log Out button (visible if logged in) -->
      <el-menu-item
        index="5"
        route="/"
        v-if="session.isLoggedIn"
        @click="session.logout()"
      >
        <img
          src="https://final-getd.s3.eu-west-2.amazonaws.com/UI_Icons/LogOut.png"
          alt="Log Out"
          class="logout-icon"
        />
      </el-menu-item>
    </div>
  </el-menu>
</template>

<script lang="ts">
// Importing necessary modules and constants
import { useSessionStore } from "~/stores/session";
import { ref, onMounted, watch } from "vue";
import axios from "axios";
import { getUserDataEndPoint } from "~/constants/endpoints";

export default {
  setup() {
    // Getting session store
    const session = useSessionStore();
    // Profile image URL
    const profileImageUrl = ref("");

    // Function to fetch user data
    const fetchUserData = async () => {
      try {
        const response = await axios.get(
          `${getUserDataEndPoint}?username=${session.username}`
        );
        // Set profile image URL
        profileImageUrl.value = `data:image/png;base64,${response.data.profilePicture}`;
      } catch (error) {
        console.error("Failed to fetch user data: ", error);
      }
    };

    // Watch for changes in username and fetch user data accordingly
    watch(
      () => session.username,
      async (username) => {
        if (username && session.isLoggedIn) {
          await fetchUserData();
        } else {
          // Clear image if not logged in or username changes
          profileImageUrl.value = "";
        }
      },
      { immediate: true } // Fetch data on initial mount
    );

    return { session, profileImageUrl };
  },
  data() {
    return {
      // Active menu index
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
  padding-left: $spacing-large;
  padding-right: $spacing-large;
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
  width: $spacing-large;
  height: $spacing-large;
  margin-left: $spacing-small;
  margin-right: $spacing-small;
}

.profile-image {
  width: 45px;
  height: 45px;
  border-radius: 50%;
}
</style>
