export default class Timer {
  public static delay(timeout: number) {
    return new Promise(resolve => {
      setTimeout(resolve, timeout);
    });
  }
}
