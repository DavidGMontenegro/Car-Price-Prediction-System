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
    <!-- List of Car Brands -->
    <h2 style="margin-top: 5vh; margin-bottom: 2.5vh">Our Car Brands</h2>
    <div class="brands">
      <el-card
        v-for="(brand, index) in carBrands"
        :key="index"
        class="brand-card"
        @click="redirectToWikipedia(brand.name)"
      >
        <template #header>
          <p class="card-header">{{ brand.name }}</p>
        </template>
        <img :src="brand.imageURL" style="width: 100%" />
        <p class="card-description">
          {{ truncateDescription(brand.description) }}
        </p>
      </el-card>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Carousel from "@/components/carousel.vue";
import { ElCard } from "element-plus";
import { getBrandsEndPoints } from "~/constants/endpoints";

export default {
  components: { Carousel, ElCard },
  data() {
    return {
      carouselImages: [], // Placeholder for carousel images
      carBrands: [], // Placeholder for car brands
    };
  },
  mounted() {
    this.fetchCarouselImages(); // Fetch carousel images
    this.fetchCarBrands(); // Fetch car brands
  },
  methods: {
    async fetchCarouselImages() {
      // Placeholder function for fetching carousel images (Replace with your own logic)
      // For example:
      this.carouselImages = [
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
      ];
    },
    async fetchCarBrands() {
      try {
        const response = await axios.get(getBrandsEndPoints);
        const carBrands = response.data;
        const promises = carBrands.map(async (brand) => {
          try {
            const response = await axios.get(
              `https://en.wikipedia.org/api/rest_v1/page/summary/${brand}`
            );
            const { thumbnail, extract } = response.data;
            return {
              name: brand,
              imageURL: thumbnail?.source
                ? thumbnail?.source
                : "https://www.seat.com.mt/content/dam/public/seat-website/carworlds/compare/default-image/ghost.png",
              description: extract,
            };
          } catch (error) {
            console.error(`Failed to fetch brand ${brand}`, error);
            return {
              name: brand,
              imageURL: null,
              description: "Description not available",
            };
          }
        });
        this.carBrands = await Promise.all(promises);
      } catch (error) {
        console.error("Failed to fetch car brands", error);
      }
    },
    truncateDescription(description) {
      const maxLength = 100; // Maximum length of description
      if (description.length > maxLength) {
        return description.substring(0, maxLength) + "..."; // Truncate and add ellipsis
      } else {
        return description;
      }
    },
    redirectToWikipedia(brandName) {
      const wikipediaURL = `https://en.wikipedia.org/wiki/${brandName}`;
      window.open(wikipediaURL, "_blank");
    },
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
    margin-bottom: 12vh;
    margin-top: 2vh;

    h1 {
      font-size: $font-size-extra-large;
      margin-bottom: $spacing-medium;
      color: $color-primary;
    }
  }

  h2 {
    font-size: $font-size-large;
    margin-bottom: $spacing-medium;
    color: $color-primary;
  }

  .content {
    display: flex;
    justify-content: space-between;
    flex-direction: row;
    align-items: flex-start;
    margin-bottom: 12vh;

    .description {
      flex: 1;
      padding: 0 $spacing-large;
    }

    .carousel {
      flex: 1;
    }
  }

  .brands {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;

    .brand-card {
      flex: 0 0 calc(25% - #{$spacing-medium * 2});
      min-width: 200px;
      margin-bottom: $spacing-large;
      background-color: $color-nav-background;
      border-color: transparent;
      cursor: pointer;
    }

    .brand-card:hover {
      box-shadow: 0px 0px 22px -3px $color-primary;
    }

    .card-header {
      color: $color-secondary;
      font-size: $font-size-medium;
      margin: 1vh;
    }

    .card-description {
      color: $color-secondary;
      font-weight: 200;
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

    .brands .brand-card {
      flex-basis: calc(90% - #{$spacing-medium * 2});
    }
  }
}
</style>
