using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class dataInput : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------
    //PUBLIC DEFINITIONS

    //Defining public materials for each region, each region uses a Material based on their regional colours
    //The choice was made based loosely on the colours of their provincial rugby team
    public Material northlandMaterial;
    public Material aucklandMaterial;
    public Material waikatoMaterial;
    public Material bayOfPlentyMaterial;
    public Material gisborneMaterial;
    public Material hawkesBayMaterial;
    public Material taranakiMaterial;
    public Material manawatuMaterial;
    public Material wellingtonMaterial; 
    public Material marlboroughMaterial;
    public Material tasmanMaterial;
    public Material westCoastMaterial;
    public Material canterburyMaterial;
    public Material otagoMaterial;
    public Material southlandMaterial;

    //Defining the public Text Asssets used to bring in the four sets of data
    //The division by Agriculture, Manufacturing and Service add up to the 'All' numbers
    //The numbers are in Billions of $NZ
    public TextAsset regionalDataAll;
    public TextAsset regionalDataAgriculture;
    public TextAsset regionalDataManufacturing;
    public TextAsset regionalDataService;

    //The objects used to instantiate the GDP monetary values display on each graph
    private TextMeshPro TMPObject;
    public GameObject textPrefabNumbers;
    public GameObject GDPValuesTotal;

    //Defining the Graph lines and parent objects used to store the lines of each line graph
    public GameObject lineGraph;

    public GameObject linesAll;
    public GameObject linesAgriculture;
    public GameObject linesManufacturing;
    public GameObject linesService;

    //Defining the points and parent objects
    public GameObject point;

    //Define public object for bar graphs
    public GameObject startingBar;

    //Global variables to allow for subtle spacing changes and the sizes of items with the Unity controls
    public float sizeOfNumbers;
    public float speedOfNumbers;
    public float gapBetweenRegions;
    public float gapBetweenYears;
    public float startingLocationInX;
    public float startingLocationInZ;

    //A variable to help identify which option has been selected in the dropdown so we know which set of lines to display or hides
    private int graphSelection;

    //Define UI objects to allow for selecttion and show/hide of visuals for each one
    public Dropdown dropdown;
    public Button displayValues;
    public Button displayLines;
    public Button displayBars;
    //public Button displayPoints;


    //Options for selecting from the drop down list
    List<string> industry = new List<string> { "Clear Selection", "Total GDP (Billions $)", "Agriculture GDP (Billions $)", "Manufacturing GDP (Billions $)", "Service GDP (Billions $)" };


    //--------------------------------------------------------------------------------------------------------------------------------------------
    //START: is called before the first frame update
    void Start()
    {
        //Declare the buttons
  
        Button displayGDPValues = displayValues.GetComponent<Button>();
        Button displayBarGraph = displayBars.GetComponent<Button>();
        Button displayLineGraph = displayLines.GetComponent<Button>();

        //Set the Dropdown options 
        dropdown.AddOptions(industry);                //Link the list of options to the dropdown object

        //When an option is changed in the dropdown 'Listen' for the change and run the 'displayIndustry' function with dropdown as the argument
        dropdown.onValueChanged.AddListener(delegate { displayIndustry(dropdown, displayGDPValues, displayBarGraph); });

        //Create all of the lines for each category using the 'drawLines' function, defining the data file and parent object to be used
        drawLines(regionalDataAll,linesAll, displayLineGraph);
        drawLines(regionalDataAgriculture,linesAgriculture, displayLineGraph);
        drawLines(regionalDataManufacturing,linesManufacturing, displayLineGraph);
        drawLines(regionalDataService,linesService, displayLineGraph);

        //Hide the parents of the Graph Lines so they are not shown initially
        linesAll.SetActive(false);
        linesAgriculture.SetActive(false);
        linesManufacturing.SetActive(false);
        linesService.SetActive(false);

    }

    //----------------------------------------------------------------------------------------------------------------------------
    //DISPLAY INDUSTRY: Depending on what is select from the dropdown perform different tasks
    void displayIndustry(Dropdown selection, Button valuesButton, Button barsButton)
    {
        //If 'Clear selection' is selected hide everything
        if (selection.value == 0)
        {
            //Hide the children of dataInput
            hide(valuesButton, barsButton);                               

            //Hide all of the lines that may have been made active previously
            linesAll.SetActive(false);            
            linesAgriculture.SetActive(false);
            linesManufacturing.SetActive(false);
            linesService.SetActive(false);

            //Set the global variable 'graphSelection' to zero to allow for others to be changed in other parts of the code
            graphSelection = 0;
        }

        //If 'Total GDP (Billions $)' is selected...
        if (selection.value == 1)
        {
            //Hide the children of dataInput
            hide(valuesButton, barsButton);

            //Hide all of the lines that may have been made active previously except for total
            linesAgriculture.SetActive(false);
            linesManufacturing.SetActive(false);
            linesService.SetActive(false);

            //Set the global variable 'graphSelection' to 1 to allow for others to be changed in other parts of the code
            graphSelection = 1;

            //Call the function to Show the Bar Graph and use the total data file as the input
            gdpShowGraph(regionalDataAll, valuesButton, barsButton);
            linesAll.SetActive(true);
        }

        //If 'Agriculture GDP (Billions $)' is selected...
        if (selection.value == 2)
        {
            //Hide the children of dataInput
            hide(valuesButton, barsButton);

            //Hide all of the lines that may have been made active previously except for Agriculture
            linesAll.SetActive(false);
            linesManufacturing.SetActive(false);
            linesService.SetActive(false);

            //Set the global variable 'graphSelection' to 2 to allow for others to be changed in other parts of the code
            graphSelection = 2;

            //Call the function to Show the Bar Graph and use the Agriculture data file as the input
            gdpShowGraph(regionalDataAgriculture, valuesButton, barsButton);
            linesAgriculture.SetActive(true);
        }

        //If 'Manufacturing GDP (Billions $)' is selected...
        if (selection.value == 3)
        {
            //Hide the children of dataInput
            hide(valuesButton, barsButton);

            //Hide all of the lines that may have been made active previously except for Manufacturing
            linesAll.SetActive(false);
            linesAgriculture.SetActive(false);
            linesService.SetActive(false);

            //Set the global variable 'graphSelection' to 3 to allow for others to be changed in other parts of the code
            graphSelection = 3;

            //Call the function to Show the Bar Graph and use the Manufacturing data file as the input
            gdpShowGraph(regionalDataManufacturing, valuesButton, barsButton);
            linesManufacturing.SetActive(true);
        }

        //If 'Service GDP (Billions $)' is selected...
        if (selection.value == 4)
        {
            //Hide the children of dataInput
            hide(valuesButton, barsButton);

            //Hide all of the lines that may have been made active previously except for Service
            linesAll.SetActive(false);
            linesAgriculture.SetActive(false);
            linesManufacturing.SetActive(false);

            //Set the global variable 'graphSelection' to 4 to allow for others to be changed in other parts of the code
            graphSelection = 4;

            //Call the function to Show the Bar Graph and use the Manufacturing data file as the input
            gdpShowGraph(regionalDataService, valuesButton, barsButton);
            linesService.SetActive(true);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------
    //HIDE ALL THE OBJECT that are children of the currently selected object, in this cast dataInput
    private void hide(Button valuesButton, Button barsButton)
    {
        Renderer[] rendererArray = this.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rendererArray) { r.enabled = false; }

        valuesButton.onClick.RemoveAllListeners();
        barsButton.onClick.RemoveAllListeners();
    }

    //--------------------------------------------------------------------------------------------------------------------
    //DISPLAY A BAR GRAPH FROM THE TEXT INPUT
    void gdpShowGraph(TextAsset dataInput, Button valuesButton, Button barsButton)
    {

        //Break the lines of the text file into an array of strings
        //Each line is a separate region
        string[] regionData = dataInput.text.Split('\n');

        //Loop around each region line until it gets the enf of the array
        for (int region = 0; region < regionData.Length; region++)
        {
            //For each line of year, break it up so that each year is a string entry in a different array
            string[] yearlyGDP = regionData[region].Split(',');

            //Based on the region set the position of the bar in the Z axis
            float barGap = gapBetweenRegions * region + startingLocationInZ;

            //Loop around each year until it gets to the end of the array
            for (int year = 0; year < yearlyGDP.Length; year++)
            {
                //Set the height of each 3D bar for the bar graph to the value of the GDP, scaled to the environment
                float height = float.Parse(yearlyGDP[year]) / 10000; 

                //Move the gaps between each year depending on which year it is
                float lineGap = gapBetweenYears * -year + startingLocationInX;   //The gap between each line going down

                //Adjust the height of the bar so it is level with the 'ground'
                float moveToLevel = height / 2 - 0.2f;

                //CREATE BARS FOR THE BAR GRAPH

                //Create a cube and set it to be a Parent of the datafile object
                GameObject newTower = GameObject.Instantiate(startingBar);
                newTower.transform.SetParent(transform);

                //Transform the Cube to the right height and width and move it to the right position
                newTower.transform.localScale = new Vector3(0.28f, height, 0.28f);
                newTower.transform.localPosition = new Vector3(lineGap, moveToLevel, barGap);
                newTower.transform.localRotation = Quaternion.identity;

                //Connect to the render of the bar from the bar graph
                Renderer renderer = newTower.GetComponent<Renderer>();

                //Based on what region it is set the appropriate material
                if (regionData[region] == regionData[0])  { renderer.material = northlandMaterial; }
                if (regionData[region] == regionData[1])  { renderer.material = aucklandMaterial; }
                if (regionData[region] == regionData[2])  { renderer.material = waikatoMaterial; }
                if (regionData[region] == regionData[3])  { renderer.material = bayOfPlentyMaterial; }
                if (regionData[region] == regionData[4])  { renderer.material = gisborneMaterial; }
                if (regionData[region] == regionData[5])  { renderer.material = hawkesBayMaterial; }
                if (regionData[region] == regionData[6])  { renderer.material = taranakiMaterial; }
                if (regionData[region] == regionData[7])  { renderer.material = manawatuMaterial; }
                if (regionData[region] == regionData[8])  { renderer.material = wellingtonMaterial; }
                if (regionData[region] == regionData[9])  { renderer.material = marlboroughMaterial; }
                if (regionData[region] == regionData[10]) { renderer.material = tasmanMaterial; }
                if (regionData[region] == regionData[11]) { renderer.material = westCoastMaterial; }
                if (regionData[region] == regionData[12]) { renderer.material = canterburyMaterial; }
                if (regionData[region] == regionData[13]) { renderer.material = otagoMaterial; }
                if (regionData[region] == regionData[14]) { renderer.material = southlandMaterial; }


                //CREATE THE NUMBERS THAT FLOAT ABOVE THE POINTS

                //Define the height of the numbers
                float numberHeight = height;
                
                //Instantiate the public Gameobject
                GameObject textValueDisplayed = GameObject.Instantiate(textPrefabNumbers);

                //Make the instantiated object a parent of the object
                textValueDisplayed.transform.SetParent(transform);

                //Set the Text Mesh Pro object to be able to manipulate the settings of the instantiated text
                TMPObject = FindObjectOfType<TextMeshPro>();

                //Set the text that is displayed to be the value of the GDP for the current region and year
                TMPObject.text = yearlyGDP[year];

                //Move the text to the right scale, location and rotation
                TMPObject.transform.localScale = new Vector3(sizeOfNumbers, sizeOfNumbers, sizeOfNumbers);
                TMPObject.transform.localPosition = new Vector3(lineGap, numberHeight, barGap);
                TMPObject.transform.localRotation = Quaternion.identity;

                // Rotating the GDP values being displayed
                rotateValues valuesSpeed = TMPObject.GetComponent<rotateValues>();
                valuesSpeed.rotateSpeed = speedOfNumbers;

                //Unenabline the values
                Switch switch1 = textValueDisplayed.GetComponent<Switch>();
                Switch switch2 = newTower.GetComponent<Switch>();
                //Switch switch4 = graphPoint.GetComponent<Switch>();

                valuesButton.onClick.AddListener(switch1.Bar);
                barsButton.onClick.AddListener(switch2.Bar);
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------
    //DRAW LINES AND POINTS: Draw the lines for the line graph 
    private void drawLines (TextAsset dataInput, GameObject linesParent, Button linesButton)
    {
        //Break the lines of the text file into an array of strings
        //Each line is a year 2000 - 2013
        string[] regionData = dataInput.text.Split('\n');

        //Loop around each year line
        for (int region = 0; region < regionData.Length; region++)
        {
            //For each line of year, break it up so that each region is string entry in a different array
            string[] yearlyGDP = regionData[region].Split(',');

            //Based on the region set the position of the bar in the Z axis
            float barGap = gapBetweenRegions * region + startingLocationInZ;

            //Create an array to add the 3D positions for the line, make it as long as the number of years
            Vector3[] positionsGraph = new Vector3[yearlyGDP.Length];

            //For each year put values in those Vector 3s
            for (int year = 0; year < positionsGraph.Length; year++)
            {
                //For the y value use the value of the GDP for that year scaled for the image
                float lineHeight = float.Parse(yearlyGDP[year]) / 10000;

                //Set the position of the Z axis to match gap between the lines
                float lineGap = gapBetweenYears * -year + startingLocationInX;

                //Add the calculated values to the Vector 3 co-ordinate
                positionsGraph[year] = new Vector3(-barGap, lineHeight, lineGap);


                //CREATE THE POINTS USED IN THE LINE GRAPH

                // Adding points
                GameObject graphPoint = GameObject.Instantiate(point);

                //Set the point as a parent of the dataInput object
                graphPoint.transform.SetParent(linesParent.transform);

                //Move the point to the correct location, set it's size and rotation
                graphPoint.transform.localPosition = new Vector3(lineGap, (lineHeight - 0.2f), barGap);
                graphPoint.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
                graphPoint.transform.localRotation = Quaternion.identity;

                //Points
                Switch switch4 = graphPoint.GetComponent<Switch>();
                linesButton.onClick.AddListener(switch4.Bar);
            }

            //Declare the instance of the line from the public game object
            GameObject lineGraphInstance = GameObject.Instantiate(lineGraph); //Bring in instance of the line prefab
            
            //Make the line instance a child of the object
            lineGraphInstance.transform.SetParent(linesParent.transform);                 //Set the line to be a child of the displayData object
            
            //Move line instance to centre 
            lineGraphInstance.transform.position = new Vector3(0, 0, 0);      //Move it to 0, 0, 0
            
            //Declare new line renderer
            LineRenderer lineGraphRenderer = lineGraphInstance.GetComponent<LineRenderer>();  //Activiate a Line Renderer to control the render of it
            
            //Tell line renderer how many points there are, where to position the points and what size to make the line
            lineGraphRenderer.positionCount = positionsGraph.Length;
            lineGraphRenderer.SetPositions(positionsGraph);
            lineGraphRenderer.widthMultiplier = 0.075f; //Set it's width

            //Set the materials for each line based on the region
            if (regionData[region] == regionData[0])  { lineGraphRenderer.material = northlandMaterial; }
            if (regionData[region] == regionData[1])  { lineGraphRenderer.material = aucklandMaterial; }
            if (regionData[region] == regionData[2])  { lineGraphRenderer.material = waikatoMaterial; }
            if (regionData[region] == regionData[3])  { lineGraphRenderer.material = bayOfPlentyMaterial; }
            if (regionData[region] == regionData[4])  { lineGraphRenderer.material = gisborneMaterial; }
            if (regionData[region] == regionData[5])  { lineGraphRenderer.material = hawkesBayMaterial; }
            if (regionData[region] == regionData[6])  { lineGraphRenderer.material = taranakiMaterial; }
            if (regionData[region] == regionData[7])  { lineGraphRenderer.material = manawatuMaterial; }
            if (regionData[region] == regionData[8])  { lineGraphRenderer.material = wellingtonMaterial; }
            if (regionData[region] == regionData[9])  { lineGraphRenderer.material = marlboroughMaterial; }
            if (regionData[region] == regionData[10]) { lineGraphRenderer.material = tasmanMaterial; }
            if (regionData[region] == regionData[11]) { lineGraphRenderer.material = westCoastMaterial; }
            if (regionData[region] == regionData[12]) { lineGraphRenderer.material = canterburyMaterial; }
            if (regionData[region] == regionData[13]) { lineGraphRenderer.material = otagoMaterial; }
            if (regionData[region] == regionData[14]) { lineGraphRenderer.material = southlandMaterial; }

            //Enable the switch to turn off the lines
            Switch switch3 = lineGraphInstance.GetComponent<Switch>();
            linesButton.onClick.AddListener(switch3.Line);

        }
    }


    // ----------------------------------------------------------------------------------------------------------------------------------
    // UPDATE: is called once per frame
    void Update()
    {
      
    }
}
