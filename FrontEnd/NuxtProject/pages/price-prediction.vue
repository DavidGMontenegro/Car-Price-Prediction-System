<template>
  <div class="container">
    <h1>Car Data Form for Price Prediction</h1>
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
    // Data properties for storing form inputs and options
    return {
      selectedMake: "",
      selectedModel: "",
      selectedColor: "",
      selectedTransmission: "",
      kilometer: null,
      engineCC: null,
      selectedDrivetrain: "",
      selectedOwner: "",
      selectedSellerType: "",
      selectedYear: null,
      makes: [],
      models: [],
      colors: [],
      drivetrains: [
        { value: "FWS", label: "FWS" },
        { value: "AWD", label: "AWD" },
        { value: "RWD", label: "RWD" },
      ],
      minYear: 1995,
      maxYear: new Date().getFullYear(),
    };
  },
  mounted() {
    // Fetch initial data when component is mounted
    this.getAllCarBrands();
  },
  methods: {
    async getAllCarBrands() {
      try {
        // Fetch all car brands and populate options for make
        const response = await fetch(getBrandsEndPoints);
        const data = await response.json();
        this.makes = data.map((brand) => ({ label: brand, value: brand }));
      } catch (error) {
        console.error("Error fetching car brands:", error);
      }
    },
    async getCarsByBrand() {
      try {
        // Fetch models based on selected make and populate options for model
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
        // Fetch colors based on selected make and model and populate options for color
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
    submitForm() {
      // Log form data on submission
      console.log("Selected Make:", this.selectedMake);
      console.log("Selected Model:", this.selectedModel);
      console.log("Selected Color:", this.selectedColor);
      console.log("Selected Transmission:", this.selectedTransmission);
      console.log("Kilometer:", this.kilometer);
      console.log("Engine cc:", this.engineCC);
      console.log("Selected Drivetrain:", this.selectedDrivetrain);
      console.log("Owner:", this.selectedOwner);
      console.log("Seller Type:", this.selectedSellerType);
      console.log("Year:", this.selectedYear);
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
</style>
