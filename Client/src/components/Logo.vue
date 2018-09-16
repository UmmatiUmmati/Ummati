<template>
  <svg width="100%" height="100%" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
    <title>Ummati logo</title>
    <desc>Ummati logo</desc>
    <symbol id="square" viewBox="0 0 11 11">
      <g class="ummati" fill="#04a5f0">
        <polygon points="9,1 9,1 1,1 1,2 9,2 9,5 1,5 1,10 2,10 2,8 5,8 5,9 3,9 3,10 8,10 8,7 7,7 7,10 6,10 6,6 9,6 9,11 0,11 0,4 8,4 8,3 0,3 0,0 9,0"/>
        <rect x="10" y="0" width="1" height="11"/>
      </g>
      <g class="diacritics" fill="#f35225">
        <rect x="2" y="6" width="1" height="1"/>
        <rect x="4" y="6" width="1" height="1"/>
      </g>
    </symbol>

    <symbol id="typography" viewBox="0 0 144 16">
      <path d="M34.98 12.42h7.64v3.26H31.26V.34h3.72v12.08zm-19.04 3.26H27.5v-3.21h-7.85V9.65h6.85V6.51h-6.85V3.56h7.85V.34H15.94v15.34zm29.85 0h11.57v-3.21H49.5V9.65h6.85V6.51H49.5V3.56h7.85V.34H45.79v15.34zM10.66 1.65a5.37 5.37 0 0 1 1.7 3.95c0 1.14-.4 2.29-1.31 3.72l-4 6.36H2.86l2.91-4.57.02-.04h-.1C2.34 10.86 0 8.64 0 5.66 0 2.38 2.6 0 6.2 0c1.75 0 3.33.58 4.46 1.65zM8.92 5.53A2.65 2.65 0 0 0 6.2 2.95 2.66 2.66 0 0 0 3.5 5.53 2.65 2.65 0 0 0 6.2 8.11c1.5 0 2.72-1.15 2.72-2.58zM69.4 8.65L63.86.35h-2.74v15.33h3.7V7.72l3.56 5.53h2.04l3.55-5.48v7.91h3.72V.34h-2.77L69.4 8.65zm45.13-5h4.94v12.03h3.72V3.65h4.9V.35h-13.56v3.3zm24.33 3.04l-2.17-.5c-1.09-.24-1.82-.59-1.82-1.52 0-.5.22-.92.64-1.2.4-.27.96-.41 1.63-.41 1.45 0 2.31.65 2.56 1.95h3.74C143.31 3.8 142.45 0 136.9 0c-1.78 0-3.3.47-4.4 1.35A4.52 4.52 0 0 0 130.82 5c0 2.4 1.41 3.83 4.43 4.48l2.55.57c1.14.22 1.7.67 1.7 1.38 0 .91-.83 1.43-2.26 1.43-1.82 0-3.02-.8-3.3-2.18h-3.72a5.4 5.4 0 0 0 2.29 4.04 8 8 0 0 0 4.62 1.29c3.86 0 6.35-1.95 6.35-4.96 0-2.35-1.38-3.65-4.61-4.35zm-56.98 9h11.57v-3.22H85.6V9.65h6.84V6.51H85.6V3.56h7.85V.34H81.88v15.34zm25.72-6.53L100.05.34H97.2v15.34h3.71v-8.6l7.51 8.6h2.89V.34h-3.72v8.82z"
            fill="#000"/>
    </symbol>

    <symbol id="portrait" viewBox="0 0 160 180">
      <use xlink:href="#square" x="40" y="32" width="80" height="80"/>
      <use xlink:href="#typography" x="3" y="130" width="154" height="16"/>
    </symbol>
    <symbol id="landscape" viewBox="0 0 328 64">
      <use xlink:href="#square" x="0" y="0" width="64" height="64"/>
      <use xlink:href="#typography" x="82" y="19" width="246" height="27"/>
    </symbol>
    <symbol id="skyscraper" viewBox="0 0 64 328">
      <use xlink:href="#square" x="0" y="264" width="64" height="64"/>
      <use xlink:href="#typography" x="-90" y="109" width="246" height="27" transform="rotate(-90 32 123)"/>
    </symbol>

    <use class="square" xlink:href="#square" x="5%" y="5%" width="90%" height="90%"/>
    <use class="portrait" xlink:href="#portrait" x="5%" y="5%" width="90%" height="90%"/>
    <use class="landscape" xlink:href="#landscape" x="5%" y="5%" width="90%" height="90%"/>
    <use class="skyscraper" xlink:href="#skyscraper" x="5%" y="5%" width="90%" height="90%"/>
  </svg>
</template>

<style lang="scss" scoped>
.square,
.landscape,
.skyscraper {
  visibility: hidden;
}
.portrait {
  visibility: visible;
}
@media (min-aspect-ratio: 1/1) {
  .portrait,
  .landscape,
  .skyscraper {
    visibility: hidden;
  }
  .square {
    visibility: visible;
  }
}
@media (min-aspect-ratio: 2/1) {
  .portrait,
  .square,
  .skyscraper {
    visibility: hidden;
  }
  .landscape {
    visibility: visible;
  }
}
@media (max-aspect-ratio: 1/2) {
  .square,
  .landscape,
  .portrait {
    visibility: hidden;
  }
  .skyscraper {
    visibility: visible;
  }
}
</style>
