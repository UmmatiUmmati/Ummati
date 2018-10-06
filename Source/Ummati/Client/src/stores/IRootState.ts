import IMainState from "@/stores/main/IMainState";
import IThemeState from "@/stores/theme/IThemeState";

export default interface IRootState {
  main: IMainState;
  theme: IThemeState;
}
