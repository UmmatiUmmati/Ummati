export default class Timer {
    static delay(timeout) {
        return new Promise(resolve => {
            setTimeout(resolve, timeout);
        });
    }
}
//# sourceMappingURL=Timer.js.map