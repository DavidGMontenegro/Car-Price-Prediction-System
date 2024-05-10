<template>
  <div>
    <div
      class="contact-button"
      @click="toggleFormVisible"
      v-show="!isFormVisible"
    >
      <span>Contact Us</span>
    </div>
    <div v-if="isFormVisible" class="contact-form" ref="form">
      <div class="close-button" @click="toggleFormVisible">
        <span>&#10005;</span>
      </div>
      <div class="form-group">
        <el-input v-model="formData.name" placeholder="Name" required />
      </div>
      <div class="form-group">
        <el-input
          v-model="formData.email"
          placeholder="Email"
          type="email"
          required
        />
      </div>
      <div class="form-group">
        <el-input
          v-model="formData.message"
          placeholder="Message"
          type="textarea"
          required
        />
      </div>
      <div class="button-container">
        <el-button type="primary" @click="submitForm">Send Message</el-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { sendEmailEndPoint } from "~/constants/endpoints";
import axios from "axios";

export default {
  data() {
    return {
      formData: {
        name: "",
        email: "",
        message: "",
      },
      isFormVisible: false,
    };
  },
  methods: {
    toggleFormVisible() {
      this.isFormVisible = !this.isFormVisible;
      if (this.isFormVisible) {
        this.$nextTick(() => {
          this.scrollToBottom();
        });
      }
    },
    async submitForm() {
      try {
        const response = await axios.post(
          `${sendEmailEndPoint}?mailFrom=dsfinalproject@outlook.com&subject=${this.formData.name}_${this.formData.email}&body=${this.formData.message}`
        );
        this.formData = {
          name: "",
          email: "",
          message: "",
        };
        this.isFormVisible = false;
        console.log(response);
      } catch (error) {
        this.$message.error("An error ocurred. Try again later...");
      }
    },
    scrollToBottom() {
      const form = this.$refs.form;
      window.scrollTo({
        top: document.body.scrollHeight,
        behavior: "smooth",
      });
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.contact-button {
  padding: $spacing-medium $spacing-large;
  background-color: $color-primary;
  color: black;
  border-radius: 5px;
  cursor: pointer;
}

.contact-form {
  max-width: 400px;
  background-color: $color-secondary;
  padding: $spacing-medium;
  padding-top: $spacing-small;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: $spacing-medium;
}

.el-input,
.el-textarea {
  border-radius: 5px;
  font-size: $font-size-small;
}

.el-button {
  width: 100%;
  padding: $spacing-small $spacing-medium;
  background-color: $color-primary;
  color: $color-secondary;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.el-button:hover {
  background-color: darken($color-primary, 10%);
}

.close-button {
  cursor: pointer;
  display: inline-block;
  color: $color-primary;
  width: 100%;
  text-align: end;
}

.close-button span {
  font-size: $font-size-medium;
}

.button-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
