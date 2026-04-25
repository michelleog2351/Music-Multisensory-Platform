window.onSpotifyWebPlaybackSDKReady = () => {
    console.log("Spotify SDK Ready!");
};

window.initializeSpotifyPlayer = (token) => {
    const player = new Spotify.Player({
        name: 'My Web Player',
        getOAuthToken: cb => { cb(token); },
        volume: 0.5
    });

    player.addListener('ready', ({ device_id }) => {
        console.log('Ready with Device ID', device_id);
        window.deviceId = device_id;
        console.log("TOKEN:", token);
        console.log("DEVICE:", window.deviceId);
        transferPlayback(token);
    });

    player.addListener('not_ready', ({ device_id }) => {
        console.log('Device ID offline', device_id);
    });

    player.addListener('initialization_error', e => console.error(e));
    player.addListener('authentication_error', e => console.error(e));
    player.addListener('account_error', e => console.error(e));
    player.addListener('playback_error', e => console.error(e));

    player.connect();
    window.spotifyPlayer = player;
};

window.transferPlayback = async (token) => {
    console.log("Transferring playback...");

    await fetch("https://api.spotify.com/v1/me/player", {
        method: "PUT",
        body: JSON.stringify({
            device_ids: [window.deviceId],
            play: false
        }),
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    });
};

window.playTrackWeb = async (token, trackUri) => {
    await fetch("https://api.spotify.com/v1/me/player/play", {
        method: "PUT",
        body: JSON.stringify({
            uris: [trackUri]
        }),
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    });
};

window.pauseTrack = () => {
    window.spotifyPlayer.pause();
};

window.resumeTrack = () => {
    window.spotifyPlayer.resume();
};
