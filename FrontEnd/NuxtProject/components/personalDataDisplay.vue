<template>
  <div class="personal-data-display">
    <div class="editing-buttons" v-if="editing">
      <el-button @click="toggleEditing" class="edit-button">
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
      <div class="placeholder">
        <img
          :src="profileImageUrl"
          alt="Placeholder"
          class="placeholder-image"
        />
        <div class="change-photo-text">
          <p>Change Photo</p>
          <input
            id="fileid"
            type="file"
            accept="image/*"
            @change="handleFileUpload"
            hidden
          />
        </div>
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
import { ref, watch } from "vue";
import axios from "axios";
import {
  getUserDataEndPoint,
  changeProfilePicEndPoint,
  modifyUserDataEndPoint,
} from "~/constants/endpoints";

const session = useSessionStore();
const username = ref(session.username);
const editing = ref(false);
const profileImageUrl = ref("");
const email = ref("");
const oldProfileImageUrl = ref("");
const editedUsername = ref("");
const editedEmail = ref("");

const fetchUserData = async () => {
  try {
    const response = await axios.get(
      `${getUserDataEndPoint}?username=${username.value}`
    );

    email.value = response.data.email;
    profileImageUrl.value = `data:image/png;base64,${response.data.profilePicture}`;
    oldProfileImageUrl.value = profileImageUrl.value;
  } catch (error) {
    console.error("Failed to fetch user data: ", error);
  }
};

fetchUserData();

const uploadProfilePicture = () => {
  const fileInput = document.getElementById("fileid");
  if (fileInput) {
    fileInput.click();
  }
};

const handleFileUpload = (event: any) => {
  const file = event.target.files[0];
  editing.value = true;
  const reader = new FileReader();

  reader.onload = (event) => {
    if (event.target != null) {
      const base64String = event.target.result as string;
      console.log("Base64 de la imagen:", base64String);
      profileImageUrl.value = base64String;
    }
  };

  reader.readAsDataURL(file);
};

const toggleEditing = () => {
  editing.value = !editing.value;
  editedEmail.value = "";
  editedUsername.value = "";
  profileImageUrl.value = oldProfileImageUrl.value;
};

const saveChanges = async () => {
  if (oldProfileImageUrl.value !== profileImageUrl.value) {
    const response = await axios.put(
      `${changeProfilePicEndPoint}?username=${username}`,
      profileImageUrl.value
    );

    console.log("Imagen actualizada: ", response);
  }

  let finalUsername =
    editedUsername.value.length === 0 ? username.value : editedUsername.value;
  let finalEmail =
    editedEmail.value.length === 0 ? email.value : editedEmail.value;

  try {
    const response = await axios.put(
      `${modifyUserDataEndPoint}?oldUsername=${username.value}&newUsername=${finalUsername}&newEmail=${finalEmail}`
    );
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
  }

  oldProfileImageUrl.value = profileImageUrl.value;
  username.value = finalUsername;
  email.value = finalEmail;
  toggleEditing();
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
