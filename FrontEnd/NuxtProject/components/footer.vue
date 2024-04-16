<template>
  <footer class="footer">
    <div class="footer-content">
      <div class="footer-left">
        <p class="contact-info">
          David García Montenegro<br />
          davidgm0928@usal.es
        </p>
        <router-link to="/account-managment" class="footer-link"
          >Log In</router-link
        >
        <router-link to="/" class="footer-link">Home</router-link>
      </div>
      <ContactForm />
    </div>
  </footer>
</template>

<script>
import ContactForm from "@/components/contactUsForm.vue";

export default {
  components: {
    ContactForm,
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.footer {
  background-color: $color-nav-background;
  padding: $spacing-medium;
  margin-top: $spacing-large;
}

.footer-content {
  display: flex;
  justify-content: space-around;
  align-items: center;
  color: white;
}

.footer-left {
  display: flex;
  flex-direction: column;

  .footer-link {
    text-decoration: none;
    color: $color-primary;
  }
}

@media (max-width: 768px) {
  .footer-content {
    flex-direction: column;
  }

  .footer-left {
    margin-bottom: $spacing-large;
  }
}
</style>
