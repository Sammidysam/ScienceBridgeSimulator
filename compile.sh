shopt -s globstar

echo "mcs -out:bin/ScienceBridgeSimulator.exe -r:System.Drawing,OpenTK src/**/*.cs"
mcs -out:bin/ScienceBridgeSimulator.exe -r:System.Drawing,OpenTK src/**/*.cs
