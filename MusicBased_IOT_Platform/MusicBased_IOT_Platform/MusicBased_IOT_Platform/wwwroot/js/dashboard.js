//async function loadData(days = 7) {
//    const response = await fetch(`/api/calibration/history?days=${days}`);
//    const data = await response.json();

//    renderChart(data);
//}

//window.OnRangeChange = function (days) {
//    loadData(days);

//    loadData();
//};
window.loadData = async function (days = 7, userId) {

    try {
        //const response = await fetch(`/api/calibration/history?days=${days}`);
        const response = await fetch(`/api/calibration/history?userId=${userId}&days=${days}`);

        if (!response.ok) {
            console.error("API failed:", response.status);
            return;
        }

        //const text = await response.text();

        //if (!text) {
        //    console.warn("Empty response");
        //    return;
        //}
        const data = await response.json();


       // const data = JSON.parse(text);

        console.log("DATA:", data);

        console.log("DAYS:", days);
        console.log("DATA LENGTH:", data.length);

        renderChart(data);

    } catch (err) {
        console.error("Error Loading data:", err);
    }
};


//function renderChart(data) {
//    d3.select("#myChart").selectAll("*").remove();

//    const width = 600;
//    const height = 300;
//    const margin = 40;

//    const svg = d3.select("#myChart")
//        .append("svg")
//        .attr("width", width)
//        .attr("height", height);

//    const x = d3.scaleTime()
//        .domain(d3.extent(data, d => new Date(d.createdAt)))
//        .range([margin, width - margin]);

//    const y = d3.scaleLinear()
//        .domain([0, d3.max(data, d => d.restingHeartRate)])
//        .range([height - margin, margin]);

//    const line = d3.line()
//        .x(d => x(new Date(d.createdAt)))
//        .y(d => y(d.restingHeartRate));

//    svg.append("path")
//        .datum(data)
//        .attr("d", line)
//        .attr("fill", "none")
//        .attr("stroke", "blue");

//    // TOOLTIP
//    svg.selectAll("circle")
//        .data(data)
//        .enter()
//        .append("circle")
//        .attr("cx", d => x(new Date(d.createdAt)))
//        .attr("cy", d => y(d.restingHeartRate))
//        .attr("r", 5)
//        .on("mouseover", function (event, d) {
//            d3.select("#tooltip")
//                .style("display", "block")
//                .html(`
//                    HR: ${d.restingHeartRate}<br/>
//                    HRV: ${d.hrv}<br/>
//                    Breathing: ${d.breathingRate}
//                `)
//                .style("left", event.pageX + "px")
//                .style("top", event.pageY + "px");
//        })
//        .on("mouseout", () => {
//            d3.select("#tooltip").style("display", "none");
//        });
//}

function renderChart(data) {
    d3.select("#myChart").selectAll("*").remove();

    if (!data || data.length === 0) {
        console.warn("No data for chart");
        return;
    }

    // ✅ Sort by date (VERY IMPORTANT)
    data.sort((a, b) => new Date(a.createdAt) - new Date(b.createdAt));

    const width = 600;
    const height = 300;
    const margin = { top: 20, right: 20, bottom: 40, left: 50 };

    const chartWidth = width - margin.left - margin.right;
    const chartHeight = height - margin.top - margin.bottom;

    const svg = d3.select("#myChart")
        .append("svg")
        .attr("width", width)
        .attr("height", height);

    const g = svg.append("g")
        .attr("transform", `translate(${margin.left},${margin.top})`);

    const x = d3.scaleTime()
        .domain(d3.extent(data, d => new Date(d.createdAt)))
        .range([0, chartWidth]);

    const y = d3.scaleLinear()
        .domain([0, d3.max(data, d => d.restingHeartRate || 0)])
        .nice()
        .range([chartHeight, 0]);

    g.append("g")
        .attr("transform", `translate(0,${chartHeight})`)
        .call(d3.axisBottom(x));

    g.append("g")
        .call(d3.axisLeft(y));

    const line = d3.line()
        .defined(d => d.restingHeartRate != null) 
        .x(d => x(new Date(d.createdAt)))
        .y(d => y(d.restingHeartRate));

    g.append("path")
        .datum(data)
        .attr("fill", "none")
        .attr("stroke", "#1DB954") 
        .attr("stroke-width", 2)
        .attr("d", line);

    const tooltip = d3.select("#tooltip");

    g.selectAll("circle")
        .data(data)
        .enter()
        .append("circle")
        .attr("cx", d => x(new Date(d.createdAt)))
        .attr("cy", d => y(d.restingHeartRate))
        .attr("r", 4)
        .on("mouseover", function (event, d) {
            tooltip
                .style("display", "block")
                .html(`
                    HR: ${d.restingHeartRate}<br/>
                    HRV: ${d.hrv}<br/>
                    Breathing: ${d.breathingRate}
                `);
        })
        .on("mousemove", function (event) {
            tooltip
                .style("left", (event.pageX + 10) + "px")
                .style("top", (event.pageY - 20) + "px");
        })
        .on("mouseout", function () {
            tooltip.style("display", "none");
        });
}

    window.focusSearch = () => {
        document.querySelector('.search-container input')?.focus();
    };
