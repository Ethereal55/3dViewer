# Используем официальный образ Unity с поддержкой Android
FROM unityci/editor:android-2022.3.11f1-base-1.0.1

# Устанавливаем рабочую директорию
WORKDIR /project

# Устанавливаем зависимости для Android
RUN apt-get update && apt-get install -y --no-install-recommends \
    openjdk-11-jdk \
    android-sdk \
    android-ndk \
    && rm -rf /var/lib/apt/lists/*

# Настройка переменных окружения
ENV ANDROID_SDK_ROOT=/usr/lib/android-sdk
ENV ANDROID_NDK_HOME=/usr/lib/android-ndk
ENV JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

# Принимаем лицензии Android SDK
RUN yes | sdkmanager --licenses && \
    sdkmanager "platform-tools" "platforms;android-33" "build-tools;33.0.0"

# Копируем необходимые файлы проекта
COPY Assets/Editor/AndroidBuildScript.cs ./Assets/Editor/
COPY ProjectSettings/ ./ProjectSettings/
COPY Packages/ ./Packages/
#Наш keystore-файл
COPY Keystore.keystore ./  

# Устанавливаем переменные для подписи (безопасный способ через аргументы)
ARG KEYSTORE_PASS
ARG KEYALIAS_PASS

# Команда сборки
CMD unity-editor \
    -batchmode \
    -nographics \
    -quit \
    -logFile /dev/stdout \
    -executeMethod AndroidBuildScript.PerformBuild \
    -keystorePass "$KEYSTORE_PASS" \
    -keyaliasPass "$KEYALIAS_PASS"