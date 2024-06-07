<template>
  <v-container>
    <v-btn
      to="/categorias"
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
      <v-card-title>Editar categoria</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="handleSubmit">
          <v-text-field
            v-model="categoria.nome"
            label="Nome"
            required
            :disabled="loadingCategoria"
          ></v-text-field>
          <v-text-field
            v-model="categoria.descricao"
            label="Descrição"
            required
            :disabled="loadingCategoria"
          ></v-text-field>
          <v-btn type="submit" color="primary">Editar</v-btn>
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
import { ref } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

const loadingCategoria = ref(true);
const categoria = ref({
  nome: "",
  descricao: "",
});

const successMessage = ref("");
const id = useRoute().params.id;
console.log(id);

const fetchCategoria = async () => {
  try {
    const response = await axios.get(
      `https://localhost:7118/api/Categorias/${id}`
    );
    categoria.value = response.data;
    loadingCategoria.value = false;
  } catch (error) {
    console.error("Erro ao procurar a categoria", error);
  }
};

const handleSubmit = async () => {
  try {
    const response = await axios.put(
      `https://localhost:7118/api/Categorias/${id}`,
      formData.value
    );
    successMessage.value = "Categoria editada com sucesso!";
    formData.value = { nome: "", descricao: "" };
  } catch (error) {
    console.error("Erro durante a criação da categoria", error);
  }
};
fetchCategoria();
</script>
