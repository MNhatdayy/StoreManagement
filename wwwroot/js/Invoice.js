<script>
    function calculateTotalPrice() {

        var chargeInput = document.getElementById('chargeInput').value;

    var chargeAmount = parseFloat(chargeInput) || 0;

    document.getElementById('totalPrice').textContent = chargeAmount.toFixed(2);
    }
</script>