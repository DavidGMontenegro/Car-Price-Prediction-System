<template>
  <div class="personal-data-display">
    <div class="editing-buttons" v-if="editing">
      <el-button @click="cancelChanges" class="edit-button">
        <img
          src="@/assets/images/GoBackArrow.png"
          alt="Cancelar"
          class="button-icon"
        />
      </el-button>
      <p>Edit My Data</p>
      <el-button @click="saveChanges" class="edit-button">
        <img src="@/assets/images/Done.png" alt="Guardar" class="button-icon" />
      </el-button>
    </div>
    <div class="profile-picture" @click="uploadProfilePicture">
      <img
        v-if="profileImage"
        :src="profileImage"
        alt="Profile Picture"
        class="image"
      />
      <div class="placeholder">
        <img
          src="@/assets/images/UserDefaultImage.jpg"
          alt="Placeholder"
          class="placeholder-image"
        />
        <div class="change-photo-text">Change Pic</div>
      </div>
    </div>
    <div class="data-item">
      <span class="data-label">Username:</span>
      <span class="data-value">{{ username }}</span>
      <el-input v-model="editedUsername" v-if="editing" class="edit-input" />
    </div>
    <div class="data-item">
      <span class="data-label">Email:</span>
      <span class="data-value">{{ email }}</span>
      <el-input v-model="editedEmail" v-if="editing" class="edit-input" />
    </div>
    <el-button
      v-if="!editing"
      @click="toggleEditing"
      class="edit-button edit-mode-button"
      >Edit My Data
    </el-button>
  </div>
</template>

<script setup lang="ts">
import { useSessionStore } from "~/stores/session";
import { ref } from "vue";

const session = useSessionStore();
const username = session.username;
const email = "email";
const profileImage = ref(null);
const editing = ref(false);
let editedUsername = username;
let editedEmail = email;

const uploadProfilePicture = () => {};

const toggleEditing = () => {
  editing.value = !editing.value;
};

const saveChanges = () => {
  toggleEditing(); // Salir del modo de edición después de guardar
};

const cancelChanges = () => {
  editedUsername = "";
  editedEmail = "";
  toggleEditing(); // Salir del modo de edición sin guardar
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.personal-data-display {
  display: flex;
  flex-direction: column;
  background-color: $color-secondary;
  padding: $spacing-medium;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  min-width: 20vw;
  min-height: 50vh;
  justify-content: center;
}

.profile-picture {
  position: relative;
  width: 150px;
  height: 150px;
  border-radius: 50%;
  overflow: hidden;
  margin: 0 auto;
  border: 2px solid $color-primary;
}

.placeholder {
  position: absolute;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  cursor: pointer;

  .change-photo-text {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: rgba(0, 0, 0, 0.7);
    color: white;
    text-align: center;
    padding: 8px;
    font-size: $font-size-small;
    display: none;
    border-radius: 4px;
  }

  .placeholder-image {
    position: relative;
    width: 150px;
    height: 150px;
    border-radius: 50%;
    overflow: hidden;
    width: 100%;
    height: auto;
  }

  &:hover .change-photo-text {
    display: block;
  }
}

.data-item {
  margin-top: $spacing-medium;

  .data-label {
    color: $color-primary;
    font-size: $font-size-medium;
  }

  .data-value {
    font-size: $font-size-medium;
  }

  .edit-input {
    width: 80%;
    padding: 5px;
    margin-top: 0px;
    margin-bottom: $spacing-small;
  }
}

.edit-button {
  background-color: $color-primary;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 8px 16px;
  margin-top: 0px;
  cursor: pointer;

  &:hover {
    background-color: darken($color-primary, 10%);
  }
}

.editing-buttons {
  display: flex;
  justify-content: space-between;
  padding: 10px;
  margin-bottom: 30px;
}

.button-icon {
  width: 24px;
  height: 24px;
}

.edit-mode-button {
  background-color: $color-primary;
  margin-top: $spacing-medium;
}
</style>
