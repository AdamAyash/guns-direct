export class Utitlities {
    static capitalize(text: string): string {
        return text.substring(0, 1).toUpperCase() + text.substring(1, text.length).toLowerCase();
    }

    static currentTimestamp(): number {
        return Math.ceil(new Date().getTime() / 1000);
    }

    static filterObject<T extends Record<string, unknown>>(obj: T) {
        return Object.fromEntries(
            Object.entries(obj).filter(([, value]) => value !== undefined && value !== null)
        );
    }
}