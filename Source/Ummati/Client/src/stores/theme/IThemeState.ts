export type Theme = "light" | "dark" | "high-contrast";

export default interface IThemeState {
  hue: number;
  theme: Theme;
}
