# RogueTechPerfFixes

This repository is a collection of performance enhancing fixes for the HBS BattleTech game. The mod provides both directly injected and dynamically injected patches, the latter using the Harmony patching library.

The original author is Mhburg, with their repo available at https://github.com/Mhburg/RogueTechPerfFixes. This repo is a fork by FrostRaptor that has been promoted into the BattleTechModders repository for sharing. Mhburg agreed to this approach in late 2020, but never completed the transfer. I've transferred my fork instead, and added the license he wished to have used (LGPL).

# Setup

Copy Directory.Build.props.template to Directory.Build.props and adjust to your install.

Build everything, RogueTechPerfFixes should fail but RogueTechPerfFixesInjector should have been built.

Execute the game to let the Injector run, verify in .modtek\ModTekPreloader.log that it indeed ran.

Build everything again, if it still fails, some third party dlls are not found where they should be (see HintPaths in RogueTechPerfFixes.cproj file).
