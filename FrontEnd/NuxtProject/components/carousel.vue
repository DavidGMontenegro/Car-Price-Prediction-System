<template>
  <div class="image-carousel">
    <el-carousel :interval="5000" arrow="always" indicator-position="outside">
      <el-carousel-item
        v-for="(item, index) in carouselImages"
        :key="index"
        class="carousel-item"
      >
        <img
          :src="item.src"
          :alt="'Car ' + (index + 1)"
          class="carousel-image"
        />
      </el-carousel-item>
    </el-carousel>
  </div>
</template>

<script>
export default {
  props: {
    carouselImages: {
      type: Array,
      required: true,
    },
  },
};
</script>

<style scoped>
.image-carousel {
  max-width: 600px;
}

.carousel-item {
  display: flex;
  justify-content: center;
  align-items: center;
}

.carousel-image {
  max-width: 100%;
  max-height: 100%;
  border-radius: 8px;
}
</style>
