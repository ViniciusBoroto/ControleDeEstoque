<template>
  <v-container>
    <v-card>
      <v-card-title>Criar categoria</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="handleSubmit">
          <v-text-field
            v-model="formData.nome"
            label="Nome"
            required
          ></v-text-field>
          <v-text-field
            v-model="formData.descricao"
            label="Descrição"
            required
          ></v-text-field>
          <v-select
            v-model="formData.categorias"
            :items="options"
            label="Selecione a categoria"
            required
            :loading="loading"
          ></v-select>
          <v-btn type="submit" color="primary">Criar</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

// Reactive form data
const formData = ref({
  nome: "",
  descricao: "",
  categoria: "",
});

// Reactive options array and loading state
const options = ref([]);
const loading = ref(false);

// Fetch options from API when the component is mounted
const fetchOptions = async () => {
  loading.value = true;
  try {
    const response = await axios.get("https://localhost:7118/api/Categorias");
    options.value = response.data.map((option) => ({
      text: option.name,
      value: option.id,
    }));
  } catch (error) {
    console.error("Error fetching options:", error);
  } finally {
    loading.value = false;
  }
};

onMounted(fetchOptions);

// Handle form submission
const handleSubmit = async () => {
  try {
    const response = await axios.post(
      "https://localhost:7118/api/Categorias",
      formData.value
    );
    console.log("Form submitted successfully:", response.data);
  } catch (error) {
    console.error("Error submitting form:", error);
  }
};
</script>

<style scoped>
/* Add custom styles if needed */
</style>
