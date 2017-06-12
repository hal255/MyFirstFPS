public static class GameEvent {

    // enemy broadcasts
    public const string ENEMY_HIT = "ENEMY_HIT";
    public const string ENEMY_HEALTH_UPDATED = "ENEMY_HEALTH_UPDATED";
    public const string ENEMY_DEAD = "ENEMY_DEAD";
    public const string ENEMY_SPEED_CHANGED = "ENEMY_SPEED_CHANGED";

    public const string BOSS_HEALTH_UPDATED = "BOSS_HEALTH_UPDATED";
    public const string BOSS_INTRO_START = "BOSS_INTRO_START";
    public const string BOSS_INTRO_END = "BOSS_INTRO_END";

    // player broadcasts
    public const string TELEPORT_PLAYER = "TELEPORT_PLAYER";
    public const string PLAYER_DEAD = "PLAYER_DEAD";
    public const string PLAYER_SPEED_CHANGED = "PLAYER_SPEED_CHANGED";
    public const string PLAYER_RIGHT_CLICK_DOWN = "PLAYER_RIGHT_CLICK_DOWN";
    public const string PLAYER_RIGHT_CLICK_UP = "PLAYER_RIGHT_CLICK_UP";
    public const string PLAYER_WEAPON_CHANGED = "PLAYER_WEAPON_CHANGED";
    public const string PLAYER_POTION_UPDATED = "PLAYER_POTION_UPDATED";

    public const string PLAYER_HEALTH_UPDATED = "PLAYER_HEALTH_UPDATED";
    public const string LEVEL_FAILED = "LEVEL_FAILED";
    public const string LEVEL_COMPLETE = "LEVEL_COMPLETE";
    public const string GAME_COMPLETE = "GAME_COMPLETE";

    // UI broadcasts
    public const string UPDATE_UI_MAP = "UPDATE_UI_MAP";
    public const string OPEN_SETTINGS = "OPEN_SETTINGS";
    public const string CLOSE_SETTINGS = "CLOSE_SETTINGS";

    // quiz game broadcasts
    public const string START_QUIZ = "START_QUIZ";
    public const string CLOSE_QUIZ = "CLOSE_QUIZ";

    // general broadcasts
    public const string PAUSE_GAME = "PAUSE_GAME";
    public const string RESUME_GAME = "RESUME_GAME";
    public const string CHANGE_BGM = "CHANGE_BGM";
    public const string WEATHER_UPDATED = "WEATHER_UPDATED";
}