<template>
  <div class="home-page">
    <div class="header">
      <h1>Welcome to Our Car Collection</h1>
      <p>Explore our selection of cars and find the perfect vehicle for you.</p>
    </div>
    <div class="content">
      <Carousel class="carousel" :carousel-images="carouselImages" />
      <div class="description">
        <h2>Discover Our Collection</h2>
        <p>
          In our catalog, you'll find a wide variety of cars, from classic
          models to the latest releases in the market. Explore our options and
          find the vehicle that suits your needs and preferences.
        </p>
        <p>
          Whether you're looking for a sleek sports car, a reliable family SUV,
          or an eco-friendly electric vehicle, we have something for everyone.
          Our experienced team is here to help you every step of the way.
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import Carousel from "@/components/Carousel.vue";

export default {
  components: { Carousel },
  data() {
    return {
      carouselImages: [
        {
          src: "https://images.unsplash.com/photo-1611651338412-8403fa6e3599?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        },
        {
          src: "https://images.unsplash.com/photo-1614162692292-7ac56d7f7f1e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        },
        {
          src: "https://images.unsplash.com/photo-1534093607318-f025413f49cb?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        },
        {
          src: "https://images.unsplash.com/photo-1613921568536-555645be4032?q=80&w=2022&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        },
      ],
    };
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.home-page {
  min-height: 100vh;
  margin: 0 auto;
  padding: $spacing-large;
  color: $color-secondary;

  .header {
    text-align: center;
    margin-bottom: 10vh;

    h1 {
      font-size: $font-size-extra-large;
      margin-bottom: $spacing-medium;
      color: $color-primary;
    }
  }

  .content {
    display: flex;
    justify-content: space-between;
    align-items: flex-start;

    .description {
      flex: 1;
      padding: 0 $spacing-large;

      h2 {
        font-size: $font-size-large;
        margin-bottom: $spacing-medium;
        color: $color-primary;
      }
    }

    .carousel {
      flex: 0.75;
    }
  }

  @media (max-width: 768px) {
    .content {
      flex-direction: column;
      align-items: center;
    }

    .description {
      width: 100%;
      max-width: 800px;
    }

    .carousel {
      width: 100%;
      max-width: 100%;
    }
  }
}
</style>
