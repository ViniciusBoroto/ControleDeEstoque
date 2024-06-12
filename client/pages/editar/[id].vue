<template>
  <v-container>
    <v-btn
      to="/"
      class="my-3"
      color="blue"
      variant="outlined"
      prepend-icon="mdi-arrow-left"
    >
      <template v-slot:prepend>
        <v-icon color="blue"></v-icon>
      </template>

      Voltar
    </v-btn>
    <v-card>
      <v-card-title>Editar produto</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="handleSubmit">
          <v-text-field
            v-model="formData.nome"
            label="Nome"
            variant="solo"
            required
          ></v-text-field>
          <v-text-field
            v-model="formData.descricao"
            label="Descrição"
            variant="solo"
            required
          ></v-text-field>
          <v-text-field
            class="limited-width-input"
            v-model="formData.estoque"
            variant="solo"
            label="Estoque"
            type="number"
            required
          ></v-text-field>
          <v-select
            v-model="formData.categoriaId"
            label="Categoria"
            :items="categorias"
            variant="solo"
            required
          ></v-select>
          <v-btn type="submit" color="primary" size="large">Editar</v-btn>
        </v-form>
        <v-alert
          class="my-5"
          v-if="successMessage"
          type="success"
          dismissible
          @click="successMessage = ''"
        >
          {{ successMessage }}
        </v-alert>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

const formData = ref({
  nome: "",
  descricao: "",
  estoque: "",
  categoriaId: "",
});

const { id } = useRoute().params;

const categorias = ref([]);
const successMessage = ref("");

const fetchProduto = async () => {
  try {
    const response = await axios.get(
      `https://localhost:7118/api/produtos/${id}`
    );
    formData.value = response.data;
  } catch (error) {
    console.error("Erro ao procurar o  produto", error);
  }
};

const handleSubmit = async () => {
  try {
    const response = await axios.put(
      `https://localhost:7118/api/produtos/${id}`,
      formData.value
    );
    successMessage.value = "Produto editado com sucesso!";
    formData.value = { nome: "", descricao: "" };
  } catch (error) {
    console.error("Erro durante a criação do produto", error);
  }
};

const fetchCategorias = async () => {
  try {
    const response = await axios.get("https://localhost:7118/api/categorias");
    categorias.value = response.data.map((cat) => ({
      title: cat.nome,
      value: cat.id,
    }));
  } catch (error) {
    console.error("Error fetching categories:", error);
  }
};
onMounted(fetchProduto);
onMounted(fetchCategorias);
</script>

<style scoped>
.limited-width-input {
  max-width: 150px;
}
</style>
