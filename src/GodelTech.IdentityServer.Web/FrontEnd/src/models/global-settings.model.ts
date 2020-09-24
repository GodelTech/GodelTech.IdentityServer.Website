export class GlobalSettings {
    key: string;
    value: string;
    isPublic: boolean;
    isSensitive: boolean;

    public static getSetting(model: GlobalSettings[], key: string): GlobalSettings {
        const item = model.find((f) => f.key === key);
        if (item.value == null) {
            item.value = "";
        }

        return item;
    }

    public static updateSetting(model: GlobalSettings[], key: string, value: string): GlobalSettings {
        const item = model.find((f) => f.key === key);
        if (item != null) {
            item.value = value;
        }

        return item;
    }
}
