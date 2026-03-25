//async function loadData(days = 7) {
//    const response = await fetch(`/api/calibration/history?days=${days}`);
//    const data = await response.json();

//    renderChart(data);
//}

//window.OnRangeChange = function (days) {
//    loadData(days);

//    loadData();
//};
window.loadData = async function (days = 7) {

    try {
        const response = await fetch(`/api/calibration/history?days=${days}`);

        if (!response.ok) {
            console.error("API failed:", response.status);
            return;
        }

        const text = await response.text();

        if (!text) {
            console.warn("Empty response");
            return;
        }

        const data = JSON.parse(text);

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
    d3.select("#chart").selectAll("*").remove();

    const width = 600;
    const height = 300;
    const margin = { top: 20, right: 20, bottom: 40, left: 50 };

    const svg = d3.select("#chart")
        .append("svg")
        .attr("width", width)
        .attr("height", height);

    const chartWidth = width - margin.left - margin.right;
    const chartHeight = height - margin.top - margin.bottom;

    const g = svg.append("g")
        .attr("transform", `translate(${margin.left},${margin.top})`);

    // 🔹 X axis (time/index)
    const x = d3.scaleBand()
        .domain(data.map((d, i) => i))
        .range([0, chartWidth])
        .padding(0.2);

    // 🔹 Y axis (heart rate)
    const y = d3.scaleLinear()
        .domain([0, d3.max(data, d => d.restingHeartRate || 0)])
        .nice()
        .range([chartHeight, 0]);

    // 🔹 Draw X axis
    g.append("g")
        .attr("transform", `translate(0,${chartHeight})`)
        .call(d3.axisBottom(x).tickFormat(i => `Day ${i + 1}`));

    // 🔹 Draw Y axis
    g.append("g")
        .call(d3.axisLeft(y));

    // 🔹 Tooltip
    const tooltip = d3.select("#tooltip");

    // 🔹 Bars
    g.selectAll("rect")
        .data(data)
        .enter()
        .append("rect")
        .attr("x", (d, i) => x(i))
        .attr("y", d => y(d.restingHeartRate || 0))
        .attr("width", x.bandwidth())
        .attr("height", d => chartHeight - y(d.restingHeartRate || 0))
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