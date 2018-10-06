<template>
  <svg :id="id" :class="classList" :aria-hidden="ariaHidden" :aria-labelledby="titleId" :aria-describedby="descriptionId" role="img">
    <title v-if="title" :id="titleId">{{title}}</title>
    <desc v-if="description" :id="descriptionId">{{description}}</desc>

    <symbol id="logo" viewBox="0 0 11 11">
      <g x="5%" y="5%" width="90%" height="90%">
        <g class="ummati">
          <polygon points="9,1 9,1 1,1 1,2 9,2 9,5 1,5 1,10 2,10 2,8 5,8 5,9 3,9 3,10 8,10 8,7 7,7 7,10 6,10 6,6 9,6 9,11 0,11 0,4 8,4 8,3 0,3 0,0 9,0"/>
          <rect x="10" y="0" width="1" height="11"/>
        </g>
        <g class="diacritics">
          <rect x="2" y="6" width="1" height="1"/>
          <rect x="4" y="6" width="1" height="1"/>
        </g>
      </g>
    </symbol>
    <symbol id="home" viewBox="0 0 100 100">
      <g transform="translate(0,-952.36218)">
        <path d="m 64.996501,971.36217 c -0.68092,0.033 -0.93548,0.53821 -1,1 l 0,9.75003 -13.375,-10.56253 c -0.50225,-0.30604 -1.03966,-0.1285 -1.25,0 l -38,30.00003 c -0.40565,0.2764 -0.51479,1.0202 -0.1875,1.4375 0.3273,0.4173 1.02027,0.4836 1.4375,0.1562 l 37.375,-29.49998 37.375005,29.49998 c 0.41723,0.3274 1.1102,0.2611 1.4375,-0.1562 0.32729,-0.4173 0.24198,-1.1709 -0.1875,-1.4375 l -14.625005,-11.5313 0,-17.65623 c -5e-5,-0.52358 -0.47642,-0.99995 -1,-1 l -8,0 z m 1,2 6,0 0,15.06253 -6,-4.75 0,-10.31253 z m -42,23.09373 -2,1.5625 0,34.3438 a 1.0001,1.0001 0 0 0 1,1 l 17,0 a 1.0001,1.0001 0 0 0 1,-1 l 0,-19 18,0 0,19 a 1.0001,1.0001 0 0 0 1,1 l 17,0 a 1.0001,1.0001 0 0 0 1,-1 l 0,-34.3438 -2,-1.5625 0,34.9063 -15,0 0,-19 a 1.0001,1.0001 0 0 0 -1,-1 l -20,0 a 1.0001,1.0001 0 0 0 -1,1 l 0,19 -15,0 0,-34.9063 z"/>
      </g>
    </symbol>
    <symbol id="notification" viewBox="0 0 100 100">
      <path d="M81.7,76.3C77.5,72.1,75,66.4,75,61V47c0-12.1-8.7-22.2-20.1-24.5c0.699-1,1.1-2.2,1.1-3.5c0-3.3-2.7-6-6-6s-6,2.7-6,6  c0,1.3,0.4,2.5,1.1,3.5C33.7,24.8,25,34.9,25,47v14c0,5.4-2.5,11.1-6.7,15.3C18,76.6,17.9,77,18.1,77.4C18.3,77.8,18.6,78,19,78  h21.1c0.5,5,4.8,9,9.9,9c5.1,0,9.4-4,9.9-9H81c0.4,0,0.8-0.2,0.9-0.6C82.1,77,82,76.6,81.7,76.3z M46,19c0-2.2,1.8-4,4-4s4,1.8,4,4  c0,1.2-0.6,2.4-1.5,3.1c-1.104-0.138-3.888-0.139-5,0C46.6,21.4,46,20.2,46,19z M50,85c-4.1,0-7.4-3.1-7.9-7H58  C57.4,81.9,54.1,85,50,85z M21.3,76c3.6-4.3,5.7-9.8,5.7-15V47c0-12.7,10.3-23,23-23s23,10.3,23,23v14c0,5.2,2.1,10.7,5.7,15H21.3z"/>
    </symbol>
    <symbol id="search" viewBox="0 0 100 100">
      <g transform="translate(0,-952.36218)">
        <path d="m 45.998399,969.36221 c -16.00342,0 -28.9984,12.99489 -28.9984,28.9984 0,16.00339 12.99497,28.99839 28.9984,28.99839 5.83145,0 11.26227,-1.7195 15.81163,-4.6873 l 10.93689,10.9369 c 2.33806,2.3381 6.16147,2.3381 8.49953,0 2.33807,-2.338 2.33807,-6.1614 0,-8.4995 l -10.93689,-10.9369 c 2.96402,-4.5474 4.68724,-9.9841 4.68724,-15.81159 0,-16.00351 -12.99498,-28.9984 -28.9984,-28.9984 z m 0,1.99989 c 14.9226,0 26.99851,12.07584 26.99851,26.99851 0,14.92259 -12.07592,26.99849 -26.99851,26.99849 -14.92259,0 -26.99851,-12.0759 -26.99851,-26.99849 0,-14.92267 12.07591,-26.99851 26.99851,-26.99851 z m 23.12372,44.4975 10.68691,10.6556 c 1.57911,1.5792 1.5791,4.0769 0,5.656 -1.57909,1.5791 -4.07684,1.5791 -5.65593,0 l -10.65567,-10.6869 c 2.11657,-1.606 4.01891,-3.5079 5.62469,-5.6247 z"/>
      </g>
    </symbol>

    <use :xlink:href="href"/>
  </svg>
</template>

<script lang="ts">
import Vue from "vue";
import Uid from "@/framework/Uid";

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
    },
    value: {
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
    href(): string {
      return "#" + this.value;
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
