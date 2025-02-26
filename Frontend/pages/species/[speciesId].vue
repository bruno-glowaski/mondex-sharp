<template>
  <v-container class="pa-8" width="md" height="100%">
    <v-card v-if="data != null" class="pa-4 self-center">
      <v-card-title class="d-flex align-center ga-2">
        <span>{{ data.name }}</span>
        <div class="ga-1">
          <v-chip
            v-for="(type, index) in data.types"
            :key="index"
            color="primary"
          >
            {{ type.name }}
          </v-chip>
        </div>
      </v-card-title>
      <v-card-subtitle>{{ data.genera }}</v-card-subtitle>
      <v-card-text>
        <v-row>
          <v-col is="p" cols="6">{{ data.description }}</v-col>
          <v-col
            is="p"
            cols="12"
            lg="6"
            class="d-flex flex-column align-strech"
          >
            <MoleculeStatDisplay
              is="li"
              label="HP"
              :value="data.base_stats?.hp!"
            />
            <MoleculeStatDisplay
              is="li"
              label="Attack"
              :value="data.base_stats?.attack!"
            />
            <MoleculeStatDisplay
              is="li"
              label="Defense"
              :value="data.base_stats?.defense!"
            />
            <MoleculeStatDisplay
              is="li"
              label="Special Attack"
              :value="data.base_stats?.special_attack!"
            />
            <MoleculeStatDisplay
              is="li"
              label="Special Defense"
              :value="data.base_stats?.special_defense!"
            />
            <MoleculeStatDisplay
              is="li"
              label="Speed"
              :value="data.base_stats?.speed!"
            />
          </v-col>
        </v-row>
      </v-card-text>
      <v-card-actions class="justify-end">
        <v-btn
          disabled
          prepend-icon="mdi-pencil"
          variant="flat"
          color="primary"
          size="large"
        >
          Edit
        </v-btn>
        <v-btn disabled prepend-icon="mdi-delete" color="error" size="large"
          >Delete</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { MoleculeStatDisplay } from "#components";

const route = useRoute();

const { data } = await useBackend("/api/species/{id}", {
  path: { id: parseInt(route.params.speciesId as string, 10) },
});
</script>
