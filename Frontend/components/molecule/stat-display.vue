<template>
  <v-row :is="tag" class="align-center ga-2">
    <v-col cols="2">
      <span class="font-weight-bold">{{ label }}</span>
    </v-col>
    <v-col cols="1">
      <span class="text-no-wrap">{{ value }}</span>
    </v-col>
    <v-col>
      <v-progress-linear
        color="primary"
        :model-value="value * CONVERSION_CONSTANT"
      />
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
export interface Props {
  is?: string | Component;
  label: string;
  value: number;
}

const CONVERSION_CONSTANT = 100 / 500;

const { is: tag = "div" } = defineProps<Props>();
</script>
