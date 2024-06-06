<template>
  <v-dialog>
    <v-container>
      <v-card>
        <v-card-title>{{
          isEditMode ? "Edit Product" : "Create Product"
        }}</v-card-title>
        <v-card-text>
          <v-form @submit.prevent="handleSubmit">
            <v-text-field
              v-model="formData.nome"
              label="Nome"
              required
            ></v-text-field>
            <v-text-field
              v-model="formData.descricao"
              label="Descricao"
              required
            ></v-text-field>
            <v-text-field
              v-model="formData.estoque"
              label="Estoque"
              type="number"
              required
            ></v-text-field>
            <v-select
              v-model="formData.categoria"
              :items="categorias"
              item-text="nome"
              item-value="id"
              label="Categoria"
              required
              :loading="loadingcategorias"
            ></v-select>
            <v-btn type="submit" color="primary">{{
              isEditMode ? "Update" : "Create"
            }}</v-btn>
          </v-form>
          <v-alert
            v-if="successMessage"
            type="success"
            dismissible
            @click:close="successMessage = ''"
          >
            {{ successMessage }}
          </v-alert>
        </v-card-text>
      </v-card>
    </v-container>
  </v-dialog>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

// Reactive data
const { InitialCategory } = defineProps(["categoria"]);
const formData = ref({
  nome: "",
  descricao: "",
  estoque: 0,
  categoria: InitialCategory,
});
const categorias = ref([]);
const loadingcategorias = ref(false);
const successMessage = ref("");
const isEditMode = ref(false);

const router = useRouter();

const fetchCategorias = async () => {
  loadingcategorias.value = true;
  try {
    const response = await axios.get("https://localhost:7118/api/Categorias");
    categorias.value = response.data;
  } catch (error) {
    console.error("Erro ao obter as categorias:", error);
  } finally {
    loadingcategorias.value = false;
  }
};

const handleSubmit = async () => {
  try {
    if (isEditMode.value) {
      await axios.put(
        `https://api.example.com/products/${formData.value.id}`,
        formData.value
      );
      successMessage.value = "Product updated successfully!";
    } else {
      await axios.post("https://api.example.com/products", formData.value);
      successMessage.value = "Product created successfully!";
      formData.value = {
        nome: "",
        descricao: "",
        estoque: 0,
        categoria: InitialCategory,
      };
    }
  } catch (error) {
    console.error("Error submitting form:", error);
  }
};

onMounted(fetchCategorias);
</script>
