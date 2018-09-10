<template>
  <svg :id="id" :class="classList" :aria-hidden="ariaHidden" :aria-labelledby="titleId" :aria-describedby="descriptionId" role="img" viewBox="0 0 512 512">
    <title v-if="title" :id="titleId">{{title}}</title>
    <desc v-if="description" :id="descriptionId">{{description}}</desc>
    <slot />
  </svg>
</template>

<script lang="ts">
import Vue from "vue";
import { Uid } from "@/framework";

export type Size =
  | "xs"
  | "sm"
  | "md"
  | "lg"
  | "xl"
  | "2x"
  | "3x"
  | "4x"
  | "5x"
  | "6x"
  | "7x"
  | "8x"
  | "9x"
  | "10x";
export const AllSizes = [
  "xs",
  "sm",
  "md",
  "lg",
  "xl",
  "2x",
  "3x",
  "4x",
  "5x",
  "6x",
  "7x",
  "8x",
  "9x",
  "10x"
];

export default Vue.extend({
  name: "Icon",
  props: {
    description: {
      default: undefined,
      type: String
    },
    size: {
      default: undefined,
      type: String as () => Size,
      validator(x: any) {
        return AllSizes.includes(x);
      }
    },
    title: {
      required: true,
      type: String
    }
  },
  computed: {
    ariaHidden(): boolean {
      return this.title ? true : false;
    },
    classList(): string {
      return this.size && this.size !== "md"
        ? `icon icon-${this.size}`
        : "icon";
    },
    descriptionId(): string | undefined {
      return this.description ? `${this.id}-description` : undefined;
    },
    id(): string {
      return Uid.newUid();
    },
    titleId(): string | undefined {
      return this.title ? `${this.id}-title` : undefined;
    }
  }
});
</script>

<style lang="scss" scoped>
$icon-size-xs: 0.75rem;
$icon-size-sm: 0.875rem;
$icon-size-md: 1rem;
$icon-size-lg: 1.33rem;
$icon-size-2x: 2rem;
$icon-size-3x: 3rem;
$icon-size-4x: 4rem;
$icon-size-5x: 5rem;
$icon-size-6x: 6rem;
$icon-size-7x: 7rem;
$icon-size-8x: 8rem;
$icon-size-9x: 9rem;
$icon-size-10x: 10rem;

$icon-sizes: (
  "xs": $icon-size-xs,
  "sm": $icon-size-sm,
  "md": $icon-size-md,
  "lg": $icon-size-lg,
  "2x": $icon-size-2x,
  "3x": $icon-size-3x,
  "4x": $icon-size-4x,
  "5x": $icon-size-5x,
  "6x": $icon-size-6x,
  "7x": $icon-size-7x,
  "8x": $icon-size-8x,
  "9x": $icon-size-9x,
  "10x": $icon-size-10x
);

.icon {
  display: inline-block;
  height: $icon-size-md;
  width: $icon-size-md;

  @each $size, $value in $icon-sizes {
    &.icon-#{$size} {
      height: $value;
      width: $value;
    }
  }
}
</style>
