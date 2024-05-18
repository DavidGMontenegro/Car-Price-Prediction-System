<template>
  <!-- Container for the entire form -->
  <div class="container">
    <!-- Title for the form -->
    <h1>Car Data Form for Price Prediction</h1>
    <!-- Form starts here -->
    <form @submit.prevent="submitForm" class="form">
      <!-- Select for Make -->
      <el-select-v2
        v-model="selectedMake"
        :options="makes"
        placeholder="Make"
        class="select"
        @change="getCarsByBrand"
      />

      <!-- Select for Model -->
      <el-select-v2
        v-model="selectedModel"
        :options="models"
        placeholder="Model"
        class="select"
        v-if="selectedMake"
        @change="getColors"
      />

      <!-- Select for Color -->
      <el-select-v2
        v-model="selectedColor"
        :options="colors"
        placeholder="Color"
        class="select"
        v-if="selectedMake && selectedModel"
      />

      <!-- Select for Transmission -->
      <div class="radio-group">
        <p>Transmission:</p>
        <el-radio v-model="selectedTransmission" label="Automatic">
          Automatic
        </el-radio>
        <el-radio v-model="selectedTransmission" label="Manual">
          Manual
        </el-radio>
      </div>

      <!-- Input for Kilometer -->
      <el-input-number
        v-model="kilometer"
        controls-position="right"
        placeholder="Kilometers"
        class="input-number"
      ></el-input-number>

      <!-- Input for Engine cc -->
      <el-input-number
        v-model="engineCC"
        controls-position="right"
        placeholder="Engine cc"
        class="input-text"
      ></el-input-number>

      <!-- Select for Drivetrain -->
      <el-select-v2
        v-model="selectedDrivetrain"
        :options="drivetrains"
        placeholder="Drivetrain"
        class="select"
      />

      <!-- Radio buttons for Owner -->
      <div class="radio-group">
        <p>Owner:</p>
        <el-radio v-model="selectedOwner" label="First"> First Owner </el-radio>
        <el-radio v-model="selectedOwner" label="Second">
          Second Owner
        </el-radio>
      </div>

      <!-- Radio buttons for Seller Type -->
      <div class="radio-group">
        <p>Seller Type:</p>
        <el-radio v-model="selectedSellerType" label="Individual">
          Individual
        </el-radio>
        <el-radio v-model="selectedSellerType" label="Corporate">
          Corporate
        </el-radio>
      </div>

      <!-- Slider for Year -->
      <el-slider
        v-model="selectedYear"
        :min="minYear"
        :max="maxYear"
        show-input
        class="slider"
      ></el-slider>

      <!-- Submit Button -->
      <button type="submit" class="submit-btn">Submit Data</button>
    </form>

    <!-- Display Predicted Price -->
    <transition name="fade">
      <div v-if="predictedPrice !== null" class="predicted-price">
        <h2>Predicted Price: {{ predictedPrice }}</h2>
      </div>
    </transition>
  </div>
</template>

<script>
import {
  getBrandsEndPoints,
  getModelsByMakeEndPoints,
  getColorsByMakeAndModelEndPoints,
} from "~/constants/endpoints";

export default {
  data() {
    return {
      selectedMake: "", // Selected car make
      selectedModel: "", // Selected car model
      selectedColor: "", // Selected car color
      selectedTransmission: "", // Selected transmission type
      kilometer: null, // Input for kilometers
      engineCC: null, // Input for engine cc
      selectedDrivetrain: "", // Selected drivetrain
      selectedOwner: "", // Selected owner type
      selectedSellerType: "", // Selected seller type
      selectedYear: null, // Selected year
      makes: [], // List of car makes
      models: [], // List of car models
      colors: [], // List of car colors
      drivetrains: [
        // List of drivetrain options
        { value: "FWS", label: "FWS" },
        { value: "AWD", label: "AWD" },
        { value: "RWD", label: "RWD" },
      ],
      minYear: 1995, // Minimum year for the slider
      maxYear: new Date().getFullYear(), // Maximum year for the slider
      predictedPrice: null, // Predicted car price
    };
  },
  mounted() {
    this.getAllCarBrands(); // Fetch car brands when the component is mounted
  },
  methods: {
    async getAllCarBrands() {
      try {
        const response = await fetch(getBrandsEndPoints);
        const data = await response.json();
        this.makes = data.map((brand) => ({ label: brand, value: brand }));
      } catch (error) {
        console.error("Error fetching car brands:", error);
      }
    },
    async getCarsByBrand() {
      try {
        if (!this.selectedMake) {
          this.models = [];
          return;
        }
        const response = await fetch(
          `${getModelsByMakeEndPoints}?make=${this.selectedMake}`
        );
        const data = await response.json();
        this.models = data.map((model) => ({ label: model, value: model }));
      } catch (error) {
        console.error("Error fetching car models:", error);
      }
    },
    async getColors() {
      try {
        if (!this.selectedMake || !this.selectedModel) {
          this.colors = [];
          return;
        }
        const response = await fetch(
          `${getColorsByMakeAndModelEndPoints}?brand=${this.selectedMake}&model=${this.selectedModel}`
        );
        const data = await response.json();
        this.colors = data.map((color) => ({ label: color, value: color }));
      } catch (error) {
        console.error("Error fetching car colors:", error);
      }
    },
    async submitForm() {
      try {
        const formData = new FormData();
        formData.append("year", this.selectedYear);
        formData.append("kilometer", this.kilometer);
        formData.append("fuel_type", this.selectedFuelType);
        formData.append("transmission", this.selectedTransmission);
        formData.append("owner", this.selectedOwner);
        formData.append("make", this.selectedMake);
        formData.append("model", this.selectedModel);
        formData.append("color", this.selectedColor);
        formData.append("seller_type", this.selectedSellerType);
        formData.append("engine", this.engineCC + "cc");
        formData.append("drivetrain", this.selectedDrivetrain);

        const response = await fetch("http://localhost:5000/predict", {
          method: "POST",
          body: formData,
        });

        if (!response.ok) {
          throw new Error("Error en la predicción");
        }

        const result = await response.json();
        alert("Price prediction: " + result.predicted_price.toFixed(2));
      } catch (error) {
        console.error("Error al enviar el formulario:", error);
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

.container {
  min-height: 100vh;
  max-width: 70vw;
  margin: $spacing-medium auto;
}

h1 {
  color: $color-primary;
}

.form {
  background-color: $color-secondary;
  padding: $spacing-large;
  border-radius: 8px;
  box-shadow: 0 0 $spacing-small rgba(0, 0, 0, 0.1);
}

.select {
  margin-bottom: $spacing-medium;
}

.radio-group {
  margin-bottom: $spacing-medium;
}

.input-number,
.input-text {
  width: 100%;
  margin-bottom: $spacing-medium;
}

.slider {
  margin-bottom: $spacing-medium;
}

.submit-btn {
  background-color: $color-primary;
  color: $color-secondary;
  padding: $spacing-small $spacing-medium;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.submit-btn:hover {
  background-color: darken($color-primary, 10%);
}

.predicted-price {
  margin-top: $spacing-large;
  padding: $spacing-large;
  background-color: lighten($color-primary, 40%);
  border-radius: 8px;
  text-align: center;
  color: $color-primary;
  font-size: 1.5em;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
