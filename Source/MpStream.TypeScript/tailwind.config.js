const colors = require('tailwindcss/colors');

module.exports = {
    purge: [],
    darkMode: false, // or 'media' or 'class'
    theme: {
        colors: {
            brandBlue: '#2b59c0',
            transparent: 'transparent',
            current: 'currentColor',
            black: colors.black,
            white: colors.white,
            gray: colors.coolGray,
            red: colors.red,
            yellow: colors.amber,
            green: colors.emerald,
            blue: colors.blue,
            indigo: colors.indigo,
            purple: colors.violet,
            pink: colors.pink,
            coolGray: colors.coolGray,
            trueGray: colors.trueGray,
            warmGray: colors.warmGray,
            orange: colors.orange,
            greenLim: colors.lime,
            teal: colors.teal,
            cyan: colors.cyan,
            lightBlue: colors.lightBlue,
            fuchsia: colors.fuchsia,
            rose: colors.rose,
            headerColor: '#080808',
            NavColor: '#171717',
            CategoryBgColor: '#1e1e1e',
            CategoryTextColor: '#a2a2a2',
            bodyBgColor: '#111111',
            filterBgColor: '#161616',
            sideBarFilterBgColor: '#191c23',
            movieTitleBar: '#252525',
            movieBorderColor: '#2e2e2e'
            

        },
        extend: {},
    },
    variants: {
        extend: {},
    },
    plugins: [],
}
