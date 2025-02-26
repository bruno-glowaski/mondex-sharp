<template>
  <v-data-table-server
    :loading="status === 'pending'"
    :headers="headers"
    :items="data ?? []"
    :items-length="data?.length ?? 0"
  >
    <template #top>
      <v-row class="pa-2">
        <v-col>
          <v-text-field
            v-model="search"
            density="compact"
            label="Search"
            prepend-inner-icon="mdi-magnify"
            variant="solo-filled"
            flat
            hide-details
          />
        </v-col>
        <v-col cols="2">
          <v-btn block disabled color="success" prepend-icon="mdi-plus">
            Create new
          </v-btn>
        </v-col>
      </v-row>
    </template>
    <template #item.types="{ value }">
      <v-chip v-for="(type, index) in value" :key="index" color="primary">
        {{ type.name }}
      </v-chip>
    </template>
    <template #item.actions="{ item }">
      <v-btn
        :to="`/species/${item.id}`"
        icon="mdi-chevron-right"
        aria-label="View"
        class="me-2"
        size="small"
        color="primary"
        variant="text"
      />
    </template>
  </v-data-table-server>
</template>

<script setup lang="ts">
import { refDebounced } from "@vueuse/core";

const search = ref("");
const debouncedSearch = refDebounced(search, 1000);

const STAT_WIDTH = "56px";

const headers = [
  { key: "id", title: "Id", fixed: true, width: 0 },
  { key: "number", title: "#", width: 0 },
  { key: "name", title: "Name" },
  { key: "genera", title: "Genera" },
  { key: "types", title: "Types" },
  { key: "base_stats.hp", title: "HP", width: STAT_WIDTH },
  { key: "base_stats.attack", title: "Atk", width: STAT_WIDTH },
  { key: "base_stats.defense", title: "Def", width: STAT_WIDTH },
  { key: "base_stats.special_attack", title: "SAtk", width: STAT_WIDTH },
  { key: "base_stats.special_defense", title: "SDef", width: STAT_WIDTH },
  { key: "base_stats.speed", title: "Spd", width: STAT_WIDTH },
  { key: "actions", fixed: true, width: "64px" },
];

const { data, status } = await useBackend("/api/species", {
  query: { q: debouncedSearch },
});
</script>
