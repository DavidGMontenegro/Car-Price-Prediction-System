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
          src: "https://final-getd.s3.eu-west-2.amazonaws.com/black_porsche_garage.avif",
        },
        {
          src: "https://final-getd.s3.eu-west-2.amazonaws.com/porsche_911_back.avif",
        },
        {
          src: "https://final-getd.s3.eu-west-2.amazonaws.com/porsche_gt3_snow.avif",
        },
        {
          src: "https://final-getd.s3.eu-west-2.amazonaws.com/sporty_gt2rs.avif",
        },
        {
          src: "https://final-getd.s3.eu-west-2.amazonaws.com/vintage_porsche.avif",
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
