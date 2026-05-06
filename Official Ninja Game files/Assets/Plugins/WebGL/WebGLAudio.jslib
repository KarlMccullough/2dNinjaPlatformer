mergeInto(LibraryManager.library, {
    WebGLResumeAudio: function () {
        if (typeof WEBAudio !== 'undefined' && WEBAudio.audioContext) {
            if (WEBAudio.audioContext.state === 'suspended') {
                WEBAudio.audioContext.resume();
            }
        }
    },
    WebGLInitAudioResume: function () {
        var resumed = false;
        function resumeOnInteraction() {
            if (resumed) return;
            if (typeof WEBAudio !== 'undefined' && WEBAudio.audioContext) {
                WEBAudio.audioContext.resume().then(function () {
                    resumed = true;
                });
            }
        }
        document.addEventListener('touchstart', resumeOnInteraction, true);
        document.addEventListener('touchend', resumeOnInteraction, true);
        document.addEventListener('click', resumeOnInteraction, true);
        document.addEventListener('keydown', resumeOnInteraction, true);
    }
});
